shader_type canvas_item;

uniform vec4 grid_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform float cell_size = 32.0;
uniform float fade_radius = 64.0;
uniform float fade_strength = 1.0;
uniform vec2 viewport_size;
uniform vec2 mouse_position;

float cubic_interpolation(float t) {
    return t * t * (3.0 - 2.0 * t);
}

void fragment() {
    vec2 uv = FRAGCOORD.xy / cell_size;
    vec2 grid = abs(fract(uv - 0.5) - 0.5) / fwidth(uv);
    float line = min(grid.x, grid.y);

    float distance_to_center = length(FRAGCOORD.xy - mouse_position);
    float fade = cubic_interpolation(clamp((distance_to_center - fade_radius) / (fade_radius * fade_strength), 0.0, 1.0));

    vec4 color = vec4(grid_color.rgb, grid_color.a * (1.0 - fade) * smoothstep(1.0, 0.0, line));
    COLOR = color;
}