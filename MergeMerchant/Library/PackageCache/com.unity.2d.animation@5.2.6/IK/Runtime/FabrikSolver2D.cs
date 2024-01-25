using System.Collections.Generic;
using UnityEngine.Profiling;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.IK
{
    /// <summary>
    /// Component for 2D Forward And Backward Reaching Inverse Kinematics (FABRIK) IK.
    /// </summary>
    [MovedFrom("UnityEngine.Experimental.U2D.IK")]
    [Solver2DMenu("Chain (FABRIK)")]
    public class FabrikSolver2D : Solver2D
    {
        const float k_MinTolerance = 0.001f;
        const int k_MinIterations = 1;

        [SerializeField]
        IKChain2D m_Chain = new IKChain2D();

        [SerializeField]
        [Range(k_MinIterations, 50)]
        int m_Iterations = 10;

        [SerializeField]
        [Range(k_MinTolerance, 0.1f)]
        float m_Tolerance = 0.01f;

        float[] m_Lengths;
        Vector2[] m_Positions;
        Vector3[] m_WorldPositions;

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
        /// Returns the number of chain in the solver.
        /// </summary>
        /// <returns>This always returns 1.</returns>
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
            {
                m_Positions = new Vector2[m_Chain.transformCount];
                m_Lengths = new float[m_Chain.transformCount - 1];
                m_WorldPositions = new Vector3[m_Chain.transformCount];
            }

            for (var i = 0; i < m_Chain.transformCount; ++i)
            {
                m_Positions[i] = GetPointOnSolverPlane(m_Chain.transforms[i].position);
            }

            for (var i = 0; i < m_Chain.transformCount - 1; ++i)
            {
                m_Lengths[i] = (m_Positions[i + 1] - m_Positions[i]).magnitude;
            }
        }

        /// <summary>
        /// DoUpdateIK override from base class.
        /// </summary>
        /// <param name="effectorPositions">Target position for the chain.</param>
        protected override void DoUpdateIK(List<Vector3> effectorPositions)
        {
            Profiler.BeginSample(nameof(FabrikSolver2D.DoUpdateIK));

            var effectorPosition = effectorPositions[0];
            effectorPosition = GetPointOnSolverPlane(effectorPosition);
            if (FABRIK2D.Solve(effectorPosition, iterations, tolerance, m_Lengths, ref m_Positions))
            {
                // Convert all plane positions to world positions
                for (var i = 0; i < m_Positions.Length; ++i)
                {
                    m_WorldPositions[i] = GetWorldPositionFromSolverPlanePoint(m_Positions[i]);
                }

                for (var i = 0; i < m_Chain.transformCount - 1; ++i)
                {
                    var startLocalPosition = m_Chain.transforms[i + 1].localPosition;
                    var endLocalPosition = m_Chain.transforms[i].InverseTransformPoint(m_WorldPositions[i + 1]);
                    m_Chain.transforms[i].localRotation *= Quaternion.FromToRotation(startLocalPosition, endLocalPosition);
                }
            }

            Profiler.EndSample();
        }
    }
}