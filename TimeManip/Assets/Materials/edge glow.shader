// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32987,y:32683,varname:node_3138,prsc:2|custl-8815-OUT,alpha-3979-OUT;n:type:ShaderForge.SFN_TexCoord,id:1926,x:32056,y:32630,varname:node_1926,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:9324,x:32235,y:32630,varname:node_9324,prsc:2,tex:e4f1fee3de32377429fd1348fae62b10,ntxv:0,isnm:False|UVIN-1926-UVOUT,TEX-6746-TEX;n:type:ShaderForge.SFN_Multiply,id:2079,x:32474,y:32732,varname:node_2079,prsc:2|A-9324-A,B-5030-RGB;n:type:ShaderForge.SFN_Multiply,id:8815,x:32773,y:32781,varname:node_8815,prsc:2|A-2079-OUT,B-380-OUT;n:type:ShaderForge.SFN_Color,id:5030,x:32254,y:32794,ptovrint:False,ptlb:node_5030,ptin:_node_5030,varname:node_5030,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2dAsset,id:6746,x:32056,y:32819,ptovrint:False,ptlb:node_6746,ptin:_node_6746,varname:node_6746,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e4f1fee3de32377429fd1348fae62b10,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Slider,id:380,x:32317,y:32991,ptovrint:False,ptlb:node_380,ptin:_node_380,varname:node_380,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:3979,x:32169,y:33166,ptovrint:False,ptlb:node_3979,ptin:_node_3979,varname:node_3979,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;proporder:5030-6746-3979-380;pass:END;sub:END;*/

Shader "Shader Forge/edge glow" {
    Properties {
        _node_5030 ("node_5030", Color) = (1,0,0,1)
        _node_6746 ("node_6746", 2D) = "black" {}
        _node_3979 ("node_3979", Range(0, 1)) = 1
        _node_380 ("node_380", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _node_5030;
            uniform sampler2D _node_6746; uniform float4 _node_6746_ST;
            uniform float _node_380;
            uniform float _node_3979;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
                float4 node_9324 = tex2D(_node_6746,TRANSFORM_TEX(i.uv0, _node_6746));
                float3 finalColor = ((node_9324.a*_node_5030.rgb)*_node_380);
                return fixed4(finalColor,_node_3979);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
