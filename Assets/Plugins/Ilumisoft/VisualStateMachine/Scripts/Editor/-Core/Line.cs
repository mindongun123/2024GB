namespace Ilumisoft.VisualStateMachine.Editor
{
    using UnityEngine;

    public struct Line
    {
        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }

        public Vector2 Direction => this.End - this.Start;

        public Line(Vector2 start, Vector2 end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Returns the min distance from the line to a given point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public float GetMinDistance(Vector2 point)
        {
            return GetMinDistanceToLine(point, this);
        }

        private bool Intersects(Line other)
        {
            return IsIntersecting(this, other);
        }

        public bool Intersects(Rect rect)
        {
            //Create border lines of the rect
            Line leftBorder = new Line(rect.min, new Vector2(rect.xMin, rect.yMax));
            Line topBorder = new Line(rect.min, new Vector2(rect.xMax, rect.yMin));
            Line rightBorder = new Line(rect.max, new Vector2(rect.xMax, rect.yMin));
            Line bottomBorder = new Line(rect.max, new Vector2(rect.xMin, rect.yMax));

            return (rect.Contains(this.Start) && rect.Contains(this.End) ||
                    Intersects(leftBorder) || Intersects(rightBorder) ||
                    Intersects(topBorder) || Intersects(bottomBorder));
        }

        /// <summary>
        /// Linearly interpolates between the start and the end point of the line by t [0,1]
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector2 Lerp(float t)
        {
            t = Mathf.Clamp01(t);

            return Vector2.Lerp(this.Start, this.End, t);
        }

        /// <summary>
        /// Returns for two given line segments whether they are intersecting or not
        /// </summary>
        /// <param name="lineA"></param>
        /// <param name="lineB"></param>
        /// <returns></returns>
        private static bool IsIntersecting(Line lineA, Line lineB)
        {
            Vector2 directionA = lineA.Direction;
            Vector2 directionB = lineB.Direction;

            float det = directionB.x * directionA.y - directionA.x * directionB.y;

            if (!Mathf.Approximately(det, 0))
            {
                float deltaX = lineA.Start.x - lineB.Start.x;
                float deltaY = lineA.Start.y - lineB.Start.y;

                float s = (1 / det) * (deltaX * directionA.y - deltaY * directionA.x);
                float t = (1 / det) * (deltaX * directionB.y - deltaY * directionB.x);

                if (s > 0 && s < 1 && t > 0 && t < 1)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the minimal distance from a given point to a line
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        private static float GetMinDistanceToLine(Vector2 point, Line line)
        {
            Vector2 v = line.Direction;

            Vector2 w = point - line.Start;

            Vector2 distance = w - (1.0f / v.magnitude) * Vector2.Dot(v, w) * (v / v.magnitude);

            return distance.magnitude;
        }
    }
}