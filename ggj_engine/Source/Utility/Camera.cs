using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ggj_engine.Source.Utility
{
    public class Camera
    {
        //For reference, Position is located at the center of the cameras viewpoint, not at the top left
        public Vector2 Position;
        public Vector2 ScreenDimensions;
        public float Zoom;

        public Camera(Vector2 position, Vector2 dimensions)
        {
            Position = position;
            ScreenDimensions = dimensions;
            Zoom = 1.5f;
        }

        /// <summary>
        /// Converts a point in world space to screen space
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2 WorldToScreen(Vector2 point)
        {
            return Vector2.Transform(point, GetViewMatrix());
        }

        /// <summary>
        /// Converts a point in screen space to world space
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector2 ScreenToWorld(Vector2 point)
        {
            return Vector2.Transform(point, Matrix.Invert(GetViewMatrix()));
        }

        /// <summary>
        /// Gets the view matrix of this camera
        /// </summary>
        /// <returns></returns>
        public Matrix GetViewMatrix()
        {
            Matrix viewMatrix = Matrix.CreateTranslation(-new Vector3(Position.X, Position.Y, 0) + new Vector3(ScreenDimensions.X / 2f, ScreenDimensions.Y / 2f, 0) / Zoom) *
                                Matrix.CreateRotationZ(0) *
                                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            return viewMatrix;
        }

        /// <summary>
        /// Gets the cameras bounds, including zoom
        /// </summary>
        /// <returns></returns>
        public Vector4 GetCameraBounds()
        {
            Vector4 v = new Vector4(Position.X - ((ScreenDimensions.X / 2f) * Zoom), Position.Y - ((ScreenDimensions.Y / 2f) * Zoom),
                Position.X + ((ScreenDimensions.X / 2f) * Zoom), Position.Y + ((ScreenDimensions.Y / 2f) * Zoom));

            return v;
        }
    }
}
