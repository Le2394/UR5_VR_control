using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intel.RealSense;
using System.Runtime.InteropServices;

public class Quad : MonoBehaviour
{
    private Pipeline pipeline;
    private Texture2D depthTexture;
    private Material depthMaterial;

    void Start()
    {
        // Initialize RealSense pipeline
        pipeline = new Pipeline();
        pipeline.Start();

        // Get the material from the object's Renderer
        depthMaterial = GetComponent<Renderer>().material;

        // Create a texture for depth data
        depthTexture = new Texture2D(640, 480, TextureFormat.RGB24, false);
        depthMaterial.mainTexture = depthTexture;
    }

    void Update()
    {
        using (FrameSet frames = pipeline.WaitForFrames())
        {
            DepthFrame depthFrame = frames.DepthFrame;

            if (depthFrame != null)
            {
                // Convert RealSense depth data to Unity Texture
                UpdateDepthTexture(depthFrame);
            }
        }
    }

    void UpdateDepthTexture(DepthFrame depthFrame)
    {
        int width = depthFrame.Width;
        int height = depthFrame.Height;

        ushort[] depthData = new ushort[width * height];
        depthFrame.CopyTo(depthData);

        Color[] pixels = new Color[width * height];
        for (int i = 0; i < depthData.Length; i++)
        {
            float depth = depthData[i] / 1000.0f; // Convert mm to meters
            pixels[i] = new Color(depth, depth, depth, 1);
        }

        depthTexture.SetPixels(pixels);
        depthTexture.Apply();
    }

    void OnDestroy()
    {
        pipeline.Stop();
    }
}
