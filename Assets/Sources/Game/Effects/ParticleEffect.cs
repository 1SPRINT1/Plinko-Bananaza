using System;
using UnityEngine;

public abstract class ParticleEffect : MonoBehaviour {

    public abstract void Execute(Action<ParticleEffect> onEffectDestroyed);

}