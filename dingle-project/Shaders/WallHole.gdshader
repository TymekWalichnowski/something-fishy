shader_type spatial;
render_mode blend_mix, depth_draw_opaque, depth_prepass_alpha;

// The player's world position (set from script)
uniform vec3 player_position;
// hole should be seen
uniform bool hole_visable;

// Controls how strict the dissolve is
uniform float alignment_max : hint_range(0.0, 1.0) = 0.98;
uniform float hole_size : hint_range(0.0, 1.0);

varying vec3 world_pos_vtx;

void vertex() {
    // Pass world position to fragment stage
    world_pos_vtx = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
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

    ALBEDO = vec3(1.0); // default wall color
}
