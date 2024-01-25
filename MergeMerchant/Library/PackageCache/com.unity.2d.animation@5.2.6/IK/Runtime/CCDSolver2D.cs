using System.Collections.Generic;
using UnityEngine.Profiling;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
    /// <summary>
    /// Component for 2D Cyclic Coordinate Descent (CCD) IK.
    /// </summary>
    [MovedFrom("UnityEngine.Experimental.U2D.IK")]
    [Solver2DMenuAttribute("Chain (CCD)")]
    public class CCDSolver2D : Solver2D
    {
        const int k_MinIterations = 1;
        const float k_MinTolerance = 0.001f;
        const float k_MinVelocity = 0.01f;
        const float k_MaxVelocity = 1f;

        [SerializeField]
        IKChain2D m_Chain = new IKChain2D();

        [SerializeField]
        [Range(k_MinIterations, 50)]
        int m_Iterations = 10;

        [SerializeField]
        [Range(k_MinTolerance, 0.1f)]
        float m_Tolerance = 0.01f;

        [SerializeField]
        [Range(0f, 1f)]
        float m_Velocity = 0.5f;

        Vector3[] m_Positions;

        /// <summary>
        /// Get and Set the solver's integration count.
        /// </summary>
        public int iterations
        {
            get => m_Iterations;
            set => m_Iterations = Mathf.Max(value, k_MinIterations);
        }

        /// <summary>
        /// Get and Set target distance tolerance.
        /// </summary>
        public float tolerance
        {
            get => m_Tolerance;
            set => m_Tolerance = Mathf.Max(value, k_MinTolerance);
        }

        /// <summary>
        /// Get and Set the solver velocity.
        /// </summary>
        public float velocity
        {
            get => m_Velocity;
            set => m_Velocity = Mathf.Clamp01(value);
        }

        /// <summary>
        /// Returns the number of chain in the solver.
        /// </summary>
        /// <returns>This always returns 1</returns>
        protected override int GetChainCount() => 1;

        /// <summary>
        /// Gets the chain in the solver by index.
        /// </summary>
        /// <param name="index">Chain index.</param>
        /// <returns>Returns IKChain2D at the index.</returns>
        public override IKChain2D GetChain(int index) => m_Chain;

        /// <summary>
        /// DoPrepare override from base class.
        /// </summary>
        protected override void DoPrepare()
        {
            if (m_Positions == null || m_Positions.Length != m_Chain.transformCount)
                m_Positions = new Vector3[m_Chain.transformCount];

            for (var i = 0; i < m_Chain.transformCount; ++i)
                m_Positions[i] = m_Chain.transforms[i].position;
        }

        /// <summary>
        /// DoUpdateIK override from base class.
        /// </summary>
        /// <param name="effectorPositions">Target position for the chain.</param>
        protected override void DoUpdateIK(List<Vector3> effectorPositions)
        {
            Profiler.BeginSample(nameof(CCDSolver2D.DoUpdateIK));

            var effectorPosition = effectorPositions[0];
            var effectorLocalPosition2D = m_Chain.transforms[0].InverseTransformPoint(effectorPosition);
            effectorPosition = m_Chain.transforms[0].TransformPoint(effectorLocalPosition2D);

            if (CCD2D.Solve(effectorPosition, GetPlaneRootTransform().forward, iterations, tolerance, Mathf.Lerp(k_MinVelocity, k_MaxVelocity, m_Velocity), ref m_Positions))
            {
                for (var i = 0; i < m_Chain.transformCount - 1; ++i)
                {
                    var startLocalPosition = m_Chain.transforms[i + 1].localPosition;
                    var endLocalPosition = m_Chain.transforms[i].InverseTransformPoint(m_Positions[i + 1]);
                    m_Chain.transforms[i].localRotation *= Quaternion.FromToRotation(startLocalPosition, endLocalPosition);
                }
            }

            Profiler.EndSample();
        }
    }
}
