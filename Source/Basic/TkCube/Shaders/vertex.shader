#version 330 core
layout (location = 0) in vec3 aPosition;
in vec4 vColor;
out vec4 fColor;
void main()
{
    gl_Position = vec4(aPosition, 1.0);    
    fColor = vColor;
}