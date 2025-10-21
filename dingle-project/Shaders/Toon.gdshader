shader_type spatial;
render_mode depth_draw_opaque, depth_prepass_alpha;

uniform sampler2D texture_albedo : source_color, filter_linear_mipmap, repeat_enable;
uniform sampler2D color_gradient;
uniform sampler2D fresnel_gradient;

float fresnel(float amount, vec3 normal, vec3 view) {
    return pow(1.0 - clamp(dot(normalize(normal), normalize(view)), 0.0, 1.0), amount);
}

void fragment() {
    float basic_fresnel = fresnel(4.0, NORMAL, VIEW);
    vec4 albedo_tex = texture(texture_albedo, UV);
    ALBEDO = albedo_tex.rgb + (basic_fresnel * texture(fresnel_gradient, vec2(basic_fresnel, 0.0)).rgb);
    ALPHA = albedo_tex.a;
}


void light() // Toon lighting
{
	float dotproduct = clamp(dot(NORMAL, LIGHT), -0.9, 0.9);
	float sample = clamp((dotproduct + 1.0) * ATTENUATION / 2.1, 0.05, 0.95);
	vec4 shaded = texture(color_gradient, vec2(sample, 0.0));
	DIFFUSE_LIGHT += clamp((shaded.rgb * LIGHT_COLOR), 0.0, 1.0);
	DIFFUSE_LIGHT += (shaded.rgb * LIGHT_COLOR);
}