shader_type spatial;
render_mode depth_draw_opaque, depth_prepass_alpha, cull_disabled;

// The player's world position (set from script)
uniform vec3 player_position;
instance uniform bool hole_visable;

// Controls how strict the dissolve is
uniform float alignment_max : hint_range(0.0, 1.0) = 0.98;
uniform float hole_size : hint_range(0.0, 1.0);

// Toon
uniform vec4 albedo : source_color;
uniform sampler2D texture_albedo : source_color, filter_linear_mipmap, repeat_enable;
uniform sampler2D color_gradient;
uniform sampler2D fresnel_gradient;

varying vec3 world_pos_vtx;

void vertex() {
    world_pos_vtx = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
}

float fresnel(float amount, vec3 normal, vec3 view)
{
    return pow((1.0 - clamp(dot(normalize(normal), normalize(view)), 0.0, 1.0)), amount);
}

void fragment() {
    vec3 world_pos = world_pos_vtx;
    vec3 camera_pos = INV_VIEW_MATRIX[3].xyz;

    vec3 cam_to_player = normalize(player_position - camera_pos);
    vec3 cam_to_frag = normalize(world_pos - camera_pos);

    float dist_camera_to_player = distance(camera_pos, player_position);
    float dist_camera_to_fragment = distance(camera_pos, world_pos);

    float alignment = dot(cam_to_player, cam_to_frag);

    if (hole_visable) {
        if (dist_camera_to_fragment < dist_camera_to_player && alignment > hole_size) {
            discard;
        }
    }

    // --- Regular shading ---
    float basic_fresnel = fresnel(4.0, NORMAL, VIEW);
    vec4 albedo_tex = texture(texture_albedo, UV);
    vec3 base_color = albedo.rgb * albedo_tex.rgb + (basic_fresnel * texture(fresnel_gradient, vec2(basic_fresnel, 0.0)).rgb);

    // --- Color visible backfaces red ---
    //if (!FRONT_FACING)
	//{
        //base_color = mix(base_color, vec3(1.0, 0.0, 0.0), 0.8);
    //}

    ALBEDO = base_color;
}

void light() {
    float dotproduct = clamp(dot(NORMAL, LIGHT), -0.9, 0.9);
    float sample = clamp((dotproduct + 1.0) * ATTENUATION / 2.1, 0.05, 0.95);
    vec4 shaded = texture(color_gradient, vec2(sample, 0.0));
    DIFFUSE_LIGHT += clamp((shaded.rgb * LIGHT_COLOR), 0.0, 1.0);
}
