shader_type spatial;
render_mode depth_draw_opaque, depth_prepass_alpha;

// The player's world position (set from script)
uniform vec3 player_position;
// hole should be seen
instance uniform bool hole_visable;

// Controls how strict the dissolve is
uniform float alignment_max : hint_range(0.0, 1.0) = 0.98;
uniform float hole_size : hint_range(0.0, 1.0);


varying vec3 world_pos_vtx;

// Toon
uniform vec4 albedo : source_color;
uniform sampler2D texture_albedo : source_color,filter_linear_mipmap,repeat_enable;
uniform sampler2D color_gradient;
uniform sampler2D fresnel_gradient;

void vertex() {
    // Pass world position to fragment stage
    world_pos_vtx = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
}

float fresnel(float amount, vec3 normal, vec3 view)
{
	return pow((1.0 - clamp(dot(normalize(normal), normalize(view)), 0.0, 1.0 )), amount);
}

void fragment() {
    vec3 world_pos = world_pos_vtx;
    vec3 camera_pos = INV_VIEW_MATRIX[3].xyz;

    // Vector from camera to player and camera to this fragment
    vec3 cam_to_player = normalize(player_position - camera_pos);
    vec3 cam_to_frag   = normalize(world_pos - camera_pos);

    // Distance checks
    float dist_camera_to_player   = distance(camera_pos, player_position);
    float dist_camera_to_fragment = distance(camera_pos, world_pos);

    // Alignment check: how close this fragment is to the camera→player line
    float alignment = dot(cam_to_player, cam_to_frag);

    // Should delete fragment
	if (hole_visable)
	{	
		if (dist_camera_to_fragment < dist_camera_to_player && alignment > hole_size) 
		{
			discard;
	    }	
	}
	
	float basic_fresnel = fresnel(4.0, NORMAL, VIEW);
	vec2 base_uv = UV;
	vec4 albedo_tex = texture(texture_albedo,base_uv);
    ALBEDO = albedo.rgb * albedo_tex.rgb + (basic_fresnel * texture(fresnel_gradient, vec2(basic_fresnel, 0.0)).rgb * 1.0);
}

void light() // Toon lighting
{
	float dotproduct = clamp(dot(NORMAL, LIGHT), -0.9, 0.9);
	float sample = clamp((dotproduct + 1.0) * ATTENUATION / 2.1, 0.05, 0.95);
	vec4 shaded = texture(color_gradient, vec2(sample, 0.0));
	DIFFUSE_LIGHT += clamp((shaded.rgb * LIGHT_COLOR), 0.0, 1.0);
	//DIFFUSE_LIGHT += (shaded.rgb * LIGHT_COLOR);
}