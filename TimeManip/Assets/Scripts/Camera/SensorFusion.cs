using UnityEngine;
using System.Collections;

public class SensorFusion : MonoBehaviour
{
	public static SensorFusion instance;

	public float predictionTime = 0.040f;
	const float MIN_TIMESTEP = 0.001f;
	const float MAX_TIMESTEP = 1f;

	Vector3 accelerometer = new Vector3();
	Vector3 gyroscope = new Vector3();
	Quaternion filterToWorldQ = new Quaternion();
	Quaternion inverseWorldToScreenQ = new Quaternion();
	Quaternion worldToScreenQ = new Quaternion();
	Quaternion originalPoseAdjustQ = new Quaternion();
	Quaternion resetQ = new Quaternion();
	float previousTime;

	// TODO
	int windowOrientation = 0;
	bool isLandscape = false;

	ComplementaryFilter filter;
	PosePredictor posePredictor;

	Quaternion currentOrientation;

	public static Quaternion GetOrientation()
	{
		return instance.currentOrientation;
	}
	public static void Recenter()
	{
		// Reduce to inverted yaw-only.
		instance.resetQ = instance.filter.GetOrientation();
		instance.resetQ.x = 0;
		instance.resetQ.y = 0;
		instance.resetQ.z *= -1;

		// Take into account extra transformations in landscape mode.
		if(instance.isLandscape)
			instance.resetQ *= instance.inverseWorldToScreenQ;

		// Take into account original pose.
		instance.resetQ *= instance.originalPoseAdjustQ;
	}
	// TODO
	public static void SetScreenTransform()
	{
		instance.worldToScreenQ = new Quaternion(0, 0, 0, 1);
		switch(instance.windowOrientation)
		{
		case 0:
			break;
		case 90:
			instance.worldToScreenQ = Quaternion.AngleAxis((-Mathf.PI / 2) * Mathf.Rad2Deg, new Vector3(0, 0, 1));
			break;
		case -90:
			instance.worldToScreenQ = Quaternion.AngleAxis((Mathf.PI / 2) * Mathf.Rad2Deg, new Vector3(0, 0, 1));
			break;
		case 180:
			// TODO
			break;
		}
		instance.inverseWorldToScreenQ = Quaternion.Inverse(instance.worldToScreenQ);
	}

	void Awake()
	{
		if(instance != null)
			UnityEngine.Debug.LogError("More than an instance of SensorFusion detected!");
		instance = this;

		if(SystemInfo.supportsGyroscope)
		{
			filter = new ComplementaryFilter(.98f);
			Input.gyro.enabled = true;
			Input.gyro.updateInterval = 0.0167f; // Set the update interval to it's highest value (60 Hz)
		}
		else
		{
			Debug.LogWarning("This device doesn't have a gyroscope! Using accelerometer only (if available)");
			filter = new ComplementaryFilter(.5f);
		}
		posePredictor = new PosePredictor();

		filterToWorldQ = Quaternion.AngleAxis((-Mathf.PI / 2) * Mathf.Rad2Deg, new Vector3(1, 0, 0));

		originalPoseAdjustQ = Quaternion.AngleAxis((-windowOrientation * Mathf.PI / 18) * Mathf.Rad2Deg, new Vector3(0, 0, 1));

		SetScreenTransform();
		if(isLandscape)
			filterToWorldQ *= inverseWorldToScreenQ;

		Recenter();
	}

	Quaternion previousRotation;
	void LateUpdate()
	{
		/*
        // DEBUG        
		Vector3 accGravity = -transform.up;
		Vector3 deltaAngles = new Vector3(Mathf.DeltaAngle(transform.rotation.eulerAngles.x, previousRotation.eulerAngles.x), 0, 0);
		previousRotation = transform.rotation;
		Vector3 rotRate = deltaAngles * 60;
		if(rotRate != Vector3.zero)
			Debug.Log(rotRate);
		//
		*/

		Vector3 accGravity = Input.acceleration; // INCLUDING GRAVITY
		//Vector3 accGravity = new Vector3(0,0,0);
		Vector3 rotRate = Input.gyro.rotationRate * Mathf.Rad2Deg;
		//Vector3 rotRate = new Vector3(Input.gyro.rotationRate.y,Input.gyro.rotationRate.z,-Input.gyro.rotationRate.x) * Mathf.Rad2Deg;

		float time = Time.time;

		float deltaS = time - previousTime;
		if(deltaS <= MIN_TIMESTEP || deltaS > MAX_TIMESTEP)
		{
			Debug.Log("Invalid timestamps detected. Time step between successive " +
				"gyroscope sensor samples is very small or not monotonic");
			previousTime = time;
			return;
		}

		accelerometer = -accGravity;
		gyroscope = rotRate;

		filter.AddAccelerationSample(accelerometer, time);
		filter.AddGyroSample(gyroscope, time);

		previousTime = time;

		//

		Quaternion orientation = instance.filter.GetOrientation();
		// Predict orientation.
		Quaternion predictedQ = instance.posePredictor.GetPrediction(orientation, instance.gyroscope, instance.previousTime);

		currentOrientation = instance.filterToWorldQ;
		currentOrientation *= instance.resetQ;
		currentOrientation *= predictedQ;
		currentOrientation *= instance.worldToScreenQ;
	}

	public class ComplementaryFilter
	{
		/*
		An implementation of a simple complementary filter, which fuses gyroscope and accelerometer data
		Accelerometer data is very noisy, but stable over the long term.
		Gyroscope data is smooth, but tends to drift over the long term.
		This fusion is relatively simple:
		1. Get orientation estimates from accelerometer by applying a low-pass filter on that data.
		2. Get orientation estimates from gyroscope by integrating over time.
		3. Combine the two estimates, weighing (1) in the long term, but (2) for the short term.
		*/

		SensorSample currentAccelSample = new SensorSample();
		SensorSample currentGyroSample = new SensorSample();
		SensorSample previousGyroSample = new SensorSample();

		// Set the quaternion to be looking in the -z direction by default.
		Quaternion filterQ = new Quaternion(1, 0, 0, 1);
		Quaternion previousFilterQ = new Quaternion();
		// Orientation based on the accelerometer.
		Quaternion accelQ = new Quaternion();
		// Whether or not the orientation has been initialized.
		bool isOrientationInitialized = false;
		// Running estimate of gravity based on the current orientation.
		Vector3 estimatedGravity = new Vector3();
		// Measured gravity based on accelerometer.
		Vector3 measuredGravity = new Vector3();

		float accelGyroFactor;

		public ComplementaryFilter(float accelGyroFactor)
		{
			previousFilterQ = filterQ;

			this.accelGyroFactor = accelGyroFactor;
		}

		public void AddAccelerationSample(Vector3 value, float time)
		{
			currentAccelSample.value = value;
			currentAccelSample.time = time;
		}
		public void AddGyroSample(Vector3 value, float time)
		{
			currentGyroSample.value = value;
			currentGyroSample.time = time;

			float delta = time - previousGyroSample.time;

			//

			if(!isOrientationInitialized)
			{
				accelQ = AccelToQuaternion(currentAccelSample.value);
				previousFilterQ = accelQ;
				isOrientationInitialized = true;
				return;
			}

			var deltaT = currentGyroSample.time -
				previousGyroSample.time;

			// Convert gyro rotation vector to a quaternion delta.
			Quaternion gyroDeltaQ = GyroToQuaternionDelta(currentGyroSample.value, deltaT);

			// filter_1 = K * (filter_0 + gyro * dT) + (1 - K) * accel.
			filterQ = previousFilterQ;
			filterQ *= gyroDeltaQ;

			// Calculate the delta between the current estimated gravity and the real
			// gravity vector from accelerometer.
			Quaternion invFilterQ = new Quaternion();
			invFilterQ = filterQ;
			invFilterQ = Quaternion.Inverse(invFilterQ);

			estimatedGravity = new Vector3(0, 0, -1);
			estimatedGravity = ApplyQuaternion(invFilterQ, estimatedGravity);
			estimatedGravity.Normalize();

			measuredGravity = currentAccelSample.value;
			measuredGravity.Normalize();

			// Compare estimated gravity with measured gravity, get the delta quaternion
			// between the two.
			Quaternion deltaQ = new Quaternion();
			deltaQ.SetFromToRotation(estimatedGravity, measuredGravity);
			deltaQ = Quaternion.Inverse(deltaQ);

			// Calculate the SLERP target: current orientation plus the measured-estimated
			// quaternion delta.
			Quaternion targetQ = new Quaternion();
			targetQ = filterQ;
			targetQ *= deltaQ;

			// SLERP factor: 0 is pure gyro, 1 is pure accel.
			filterQ = Quaternion.Slerp(filterQ, targetQ, 1 - accelGyroFactor);

			previousFilterQ = filterQ;

			//

			previousGyroSample = currentGyroSample;
		}

		public Quaternion GetOrientation()
		{
			return this.filterQ;
		}
		public Quaternion AccelToQuaternion(Vector3 accel)
		{
			Vector3 normAccel = accel;
			normAccel.Normalize();
			Quaternion quat = new Quaternion();
			quat.SetFromToRotation(new Vector3(0, 0, -1), normAccel);
			quat = Quaternion.Inverse(quat);
			return quat;
		}
		public Quaternion GyroToQuaternionDelta(Vector3 gyro, float dt)
		{
			// Extract axis and angle from the gyroscope data.
			Quaternion quat = new Quaternion();
			Vector3 axis = gyro;
			axis.Normalize();
			quat = Quaternion.AngleAxis(gyro.magnitude * dt, axis);
			return quat;
		}
		public Vector3 ApplyQuaternion(Quaternion q, Vector3 v)
		{
			Vector3 o = new Vector3();

			// calculate quat * vector
			var ix = q.w * v.x + q.y * v.z - q.z * v.y;
			var iy = q.w * v.y + q.z * v.x - q.x * v.z;
			var iz = q.w * v.z + q.x * v.y - q.y * v.x;
			var iw = -q.x * v.x - q.y * v.y - q.z * v.z;

			// calculate result * inverse quat
			o.x = ix * q.w + iw * -q.x + iy * -q.z - iz * -q.y;
			o.y = iy * q.w + iw * -q.y + iz * -q.x - ix * -q.z;
			o.z = iz * q.w + iw * -q.z + ix * -q.y - iy * -q.x;

			return o;
		}
	}

	public class PosePredictor
	{
		/*
		Given an orientation and the gyroscope data, predicts the future orientation. This makes rendering appear faster.
		
		Also see: http://msl.cs.uiuc.edu/~lavalle/papers/LavYerKatAnt14.pdf
		*/

		Quaternion previousQ = new Quaternion();
		float previousTime;
		Quaternion deltaQ = new Quaternion();
		Quaternion outQ = new Quaternion();

		public PosePredictor()
		{
		}

		public Quaternion GetPrediction(Quaternion currentQ, Vector3 gyro, float time)
		{
			if(previousTime == 0)
			{
				previousQ = currentQ;
				previousTime = time;
				return currentQ;
			}

			// Calculate axis and angle based on gyroscope rotation rate data.
			Vector3 axis = new Vector3();
			axis = gyro;
			axis.Normalize();

			float angularSpeed = gyro.magnitude;

			// If we're rotating slowly, don't do prediction.
			if(angularSpeed < 20)
			{
				outQ = currentQ;
				previousQ = currentQ;
				return outQ;
			}

			// Get the predicted angle based on the time delta and latency.
			//float deltaT = time - previousTime;
			float predictAngle = angularSpeed * instance.predictionTime;

			deltaQ = Quaternion.AngleAxis(predictAngle, axis);
			outQ = previousQ;
			outQ *= deltaQ;

			previousQ = currentQ;
			previousTime = time;

			return outQ;
		}
	}

	public struct SensorSample
	{
		public Vector3 value;
		public float time;
	}
}