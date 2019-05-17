// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "InvisibleRoom/Stereo"
{
	Properties
	{
		_Offset("Offset", Range( 0 , 1)) = 0
		_InViewImage("InViewImage", 2D) = "white" {}
		_OutViewImage("OutViewImage", 2D) = "black" {}
	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform float _Offset;
			uniform sampler2D _InViewImage;
			uniform sampler2D _OutViewImage;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue =  float3(0,0,0) ;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float temp_output_32_0 = ( i.ase_texcoord.xy.x + _Offset );
				float4 color28 = IsGammaSpace() ? float4(0,0,0,0) : float4(0,0,0,0);
				float2 appendResult31 = (float2(temp_output_32_0 , i.ase_texcoord.xy.y));
				float4 ifLocalVar27 = 0;
				if( temp_output_32_0 > 0.0 )
				ifLocalVar27 = tex2D( _InViewImage, appendResult31 );
				else if( temp_output_32_0 < 0.0 )
				ifLocalVar27 = color28;
				float4 ifLocalVar34 = 0;
				if( temp_output_32_0 > 1.0 )
				ifLocalVar34 = color28;
				else if( temp_output_32_0 < 1.0 )
				ifLocalVar34 = ifLocalVar27;
				float temp_output_39_0 = ( temp_output_32_0 + -1.0 );
				float2 appendResult38 = (float2(temp_output_39_0 , i.ase_texcoord.xy.y));
				float4 ifLocalVar35 = 0;
				if( temp_output_39_0 > 0.0 )
				ifLocalVar35 = tex2D( _OutViewImage, appendResult38 );
				float4 ifLocalVar36 = 0;
				if( temp_output_39_0 < 1.0 )
				ifLocalVar36 = ifLocalVar35;
				
				
<<<<<<< HEAD
				finalColor = ( ( 1.0 - pow( ( length( ( i.ase_texcoord.xy + float2( -0.5,-0.5 ) ) ) / 0.6 ) , 8.61 ) ) * ( ifLocalVar34 + ifLocalVar36 ) );
=======
				finalColor = ( ( 1.0 - pow( ( length( ( i.ase_texcoord.xy + float2( -0.5,-0.5 ) ) ) / 0.7 ) , 8.61 ) ) * ( ifLocalVar34 + ifLocalVar36 ) );
>>>>>>> f2825b7f5bc8de43be874d0ce146de991f991870
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16700
<<<<<<< HEAD
62;1;1262;1051;304.6663;467.9185;1.3;True;False
=======
7;1;1452;894;20.61621;720.7687;1.3;True;True
>>>>>>> f2825b7f5bc8de43be874d0ce146de991f991870
Node;AmplifyShaderEditor.TexCoordVertexDataNode;30;-1192.577,-261.9927;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-1205.683,-20.86602;Float;False;Property;_Offset;Offset;0;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-972.0811,-50.81401;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1097.592,305.1883;Float;False;Constant;_constOne;constOne;3;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-869.3333,198.0464;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;45;279.6836,-172.8186;Float;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;False;0;-0.5,-0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexCoordVertexDataNode;42;261.4837,-278.1185;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;31;-672.482,15.95564;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;38;-723.3718,306.7413;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;466.8838,-232.6186;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
<<<<<<< HEAD
Node;AmplifyShaderEditor.RangedFloatNode;48;508.4836,-1.21859;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;0.6;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-490.6834,-207.5893;Float;True;Property;_InViewImage;InViewImage;1;0;Create;True;0;0;False;0;None;e2d5121b7daf4fb4b96525f391c75a6a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-519.8908,457.0325;Float;True;Property;_OutViewImage;OutViewImage;2;0;Create;True;0;0;False;0;None;e2d5121b7daf4fb4b96525f391c75a6a;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;28;-393.5506,131.8973;Float;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
=======
>>>>>>> f2825b7f5bc8de43be874d0ce146de991f991870
Node;AmplifyShaderEditor.LengthOpNode;43;602.0836,-232.6186;Float;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;28;-393.5506,131.8973;Float;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-519.8908,457.0325;Float;True;Property;_OutViewImage;OutViewImage;2;0;Create;True;0;0;False;0;None;e90f9f4e141e198438a198ec66567628;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-490.6834,-207.5893;Float;True;Property;_InViewImage;InViewImage;1;0;Create;True;0;0;False;0;None;e2d5121b7daf4fb4b96525f391c75a6a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;612.4838,46.8814;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;27;-38.03537,-81.46258;Float;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;47;778.8835,-215.7186;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;35;-94.20586,418.4818;Float;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;49;919.2837,-220.9186;Float;True;2;0;FLOAT;0;False;1;FLOAT;8.61;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;34;203.3759,-46.15547;Float;True;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;36;147.2054,453.7889;Float;True;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;53;1151.984,-201.4186;Float;True;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;587.0544,342.9684;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;1133.784,55.9814;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1363.52,36.33263;Float;False;True;2;Float;ASEMaterialInspector;0;1;InvisibleRoom/Stereo;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;0;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;32;0;30;1
WireConnection;32;1;12;0
WireConnection;39;0;32;0
WireConnection;39;1;40;0
WireConnection;31;0;32;0
WireConnection;31;1;30;2
WireConnection;38;0;39;0
WireConnection;38;1;30;2
WireConnection;44;0;42;0
WireConnection;44;1;45;0
<<<<<<< HEAD
WireConnection;1;1;31;0
WireConnection;2;1;38;0
WireConnection;43;0;44;0
=======
WireConnection;43;0;44;0
WireConnection;2;1;38;0
WireConnection;1;1;31;0
>>>>>>> f2825b7f5bc8de43be874d0ce146de991f991870
WireConnection;27;0;32;0
WireConnection;27;2;1;0
WireConnection;27;4;28;0
WireConnection;47;0;43;0
WireConnection;47;1;48;0
WireConnection;35;0;39;0
WireConnection;35;2;2;0
WireConnection;49;0;47;0
WireConnection;34;0;32;0
WireConnection;34;2;28;0
WireConnection;34;4;27;0
WireConnection;36;0;39;0
WireConnection;36;4;35;0
WireConnection;53;1;49;0
WireConnection;37;0;34;0
WireConnection;37;1;36;0
WireConnection;46;0;53;0
WireConnection;46;1;37;0
WireConnection;0;0;46;0
ASEEND*/
<<<<<<< HEAD
//CHKSM=C9E3CB2412581DE7DE493AE0BE4F99CFC48C7BA0
=======
//CHKSM=205BAAA03A2079DD6E7A03BA30FBE76FB57B453D
>>>>>>> f2825b7f5bc8de43be874d0ce146de991f991870
