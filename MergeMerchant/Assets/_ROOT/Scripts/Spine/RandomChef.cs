using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RandomChef : MonoBehaviour
{
	private sealed class _PlayAnimation_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _random___0;

		internal RandomChef _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _PlayAnimation_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				do
				{
					this._random___0 = UnityEngine.Random.Range(0, this._this.animations.Length);
				}
				while (this._random___0 == this._this.lastIndex);
				this._this.lastIndex = this._random___0;
				this._this.animator.AnimationState.SetAnimation(0, this._this.animations[this._random___0], true);
				this._current = this._this.waitForSeconds;
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.StartCoroutine(this._this.PlayAnimation());
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private int lastIndex;

	private SkeletonGraphic animator;

	private WaitForSeconds waitForSeconds;

	[SerializeField]
	private int changeTime;

	private string[] animations = new string[]
	{
		"Making_01",
		"Making_02",
		"Making_03",
		"Making_04"
	};

	private void Start()
	{
		this.animator = base.GetComponent<SkeletonGraphic>();
		this.waitForSeconds = new WaitForSeconds((float)this.changeTime);
		base.StartCoroutine(this.PlayAnimation());
	}

	public void SetSpeed(int value)
	{
		this.animator.timeScale = (float)value;
	}

	private IEnumerator PlayAnimation()
	{
		RandomChef._PlayAnimation_c__Iterator0 _PlayAnimation_c__Iterator = new RandomChef._PlayAnimation_c__Iterator0();
		_PlayAnimation_c__Iterator._this = this;
		return _PlayAnimation_c__Iterator;
	}
}
