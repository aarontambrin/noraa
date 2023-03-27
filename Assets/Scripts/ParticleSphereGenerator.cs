using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSphereGenerator : MonoBehaviour {

    public bool rewriteVertexStreams = true;
    public float particleSize = 1f;
    public Color particleColor = Color.white;
    public Vector3 particleRotation3D;
    public bool randomColorAlpha = true; // For MetallicSmoothness random offset
    public float xDistance = 0.25f;
    public float yDistance = 0.25f;
    public float zDistance = 0.25f;
    public float radius = 32f;
    public int numPoints = 128;
    public float OffsetEven = 0.125f;
    public bool updateEveryFrame = false;

    private float even;
    private Vector3[] positions;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private List<Vector4> customData = new List<Vector4>();
    private List<Vector4> customData2 = new List<Vector4>();

    void Start () {
        ps = GetComponent<ParticleSystem>();
        UpdateSpherePoints();
    }

    private void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        UpdateSpherePoints();
    }

    public void UpdateSpherePoints()
    {
        GenerateSpherePoints(numPoints);
        GenerateParticles();
        CreateOffsetVector();

        ParticleSystemRenderer psrend = GetComponent<ParticleSystemRenderer>();
        if (rewriteVertexStreams == true)
        {
            psrend.SetActiveVertexStreams(new List<ParticleSystemVertexStream>(new ParticleSystemVertexStream[] { ParticleSystemVertexStream.Position, ParticleSystemVertexStream.Normal, ParticleSystemVertexStream.Color, ParticleSystemVertexStream.UV, ParticleSystemVertexStream.Center, ParticleSystemVertexStream.Tangent, ParticleSystemVertexStream.Custom1XYZ }));

        }
        psrend.alignment = ParticleSystemRenderSpace.Local;
    }

    // Generating array of positions
    private void GenerateSpherePoints(int n)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = 0;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            upts.Add(new Vector3(x, y, z) * radius);
        }
        positions = upts.ToArray();
    }

    private Vector3 GetRotationBetweenVectors(Vector3 v1)
    {
        Quaternion rot = Quaternion.LookRotation(v1);
        return rot.ToEulerAngles() * Mathf.Rad2Deg;
    }

    // Generating particles with grid based positions
    private void GenerateParticles()
    {
        particles = new ParticleSystem.Particle[numPoints];
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].position = positions[i];
            if (randomColorAlpha == true)
            {
                particleColor.a = Random.Range(0f, 1f);
            }
            particles[i].startColor = particleColor;
            particles[i].startSize = particleSize;
            particles[i].rotation3D = GetRotationBetweenVectors(positions[i]) + particleRotation3D;
        }
        ps.SetParticles(particles, particles.Length);
    }

    // Creating Vector for Offset
    private void CreateOffsetVector()
    {
        ps.GetCustomParticleData(customData, ParticleSystemCustomData.Custom1);        

        for (int i = 0; i < particles.Length; i++)
        {
            customData[i] = positions[i];
        }

        ps.SetCustomParticleData(customData, ParticleSystemCustomData.Custom1);        
    }

    private void FixedUpdate()
    {
        if (updateEveryFrame == true)
        {
            UpdateSpherePoints();
        }
    }
}
