
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class SpellCaster_Struggler : SpellCaster
{

    public bool casting = false;
    public Dictionary<Spell, StrugglerSpellWrapper> SpellWrapperBySpellDictionary = new Dictionary<Spell, StrugglerSpellWrapper>();

    public event Action<Spell> OnSpellAdded = delegate { };
    public event Action<Spell> OnSpellRemoved = delegate { };
    public event Action OnTriggerCastTargetAnimation = delegate { };

    public AnimationEvents_Struggler AnimationEvents;
    AttackModule AttackModule;


    void Start()
    {
        SubscribeToInputEvents();
        if (AnimationEvents)
        {
            AnimationEvents.OnSetCastingToFalse += SetCastingToFalse;
            AnimationEvents.OnSetCastingToTrue += SetCastingToTrue;
        }

        AttackModule = GetComponent<AttackModule>();

    }

    void Update()
    {
        HandleSpellsCooldown();
    
    }

    void OnDisable()
    {
        OwnedSpells.Clear();
    }

    #region Public Methods
    public override void AddSpell(Spell x)
    {
        base.AddSpell(x);
        SpellWrapperBySpellDictionary.Add(x, new StrugglerSpellWrapper(x));
        OnSpellAdded(x);
    }

    public override void RemoveSpell(Spell x)
    {
        base.RemoveSpell(x);
        SpellWrapperBySpellDictionary.Remove(x);
        OnSpellRemoved(x);
    }

    public override void UseSpell(int spellIndex)
    {
        if (OwnedSpells.Count > spellIndex)
        {
            StrugglerSpellWrapper spellWrapper = SpellWrapperBySpellDictionary[OwnedSpells[spellIndex]];
            if (!spellWrapper.isOnCooldown && !casting && !AttackModule.attacking)
            {
                Spell x = OwnedSpells[spellIndex];
                x.Use(this);
                spellWrapper.SetOnCooldown();
                if(x.TriggerCastTargetAnimation)
                {
                    OnTriggerCastTargetAnimation();
                }
            }
        }
    }


    public override void UseSpellOneTime(Spell x)
    {
        x.Use(this);

        if (x.TriggerCastTargetAnimation)
        {
            OnTriggerCastTargetAnimation();
        }
        
    }


    public void ResetCooldown(Spell x)
    {
        SpellWrapperBySpellDictionary[x].StopCooldown();
    }
    #endregion


    #region Private Methods

    void SetCastingToTrue()
    {
        casting = true;
    }

    void SetCastingToFalse()
    {
        casting = false;
    }

    void SpellKeyPressed(int x)
    {

    }

    void KeyPressedQ()
    {
        UseSpell(0);
    }

    void KeyPressedE()
    {
        UseSpell(1);
    }

    void KeyPressedR()
    {
        UseSpell(2);
    }

    void KeyPressedF()
    {
        UseSpell(3);
    }

    void HandleSpellsCooldown()
    {
        if (OwnedSpells.Count > 0)
        {
            foreach (Spell x in OwnedSpells)
            {
                SpellWrapperBySpellDictionary[x].HandleCooldown();
            }
        }
    }

    void SubscribeToInputEvents()
    {
        InputModule x = GetComponent<InputModule>();
        if (x)
        {
            x.OnKeyDownE += KeyPressedE;
            x.OnKeyDownQ += KeyPressedQ;
            x.OnKeyDownR += KeyPressedR;
            x.OnKeyDownF += KeyPressedF;
        }
    }

    #endregion



    public class StrugglerSpellWrapper
    {
        public Spell spell;
        public bool isOnCooldown;
        public float cooldownTimer;

        public event Action<StrugglerSpellWrapper> OnSpellCooldownChange = delegate { };

        public void HandleCooldown()
        {
            if (isOnCooldown)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    isOnCooldown = false;
                }
                OnSpellCooldownChange(this);
            }
        }

        public void StopCooldown()
        {
            if(isOnCooldown)
            {
                cooldownTimer = 0;
            }
        }

        public void SetOnCooldown()
        {
            isOnCooldown = true;
            cooldownTimer = spell.Cooldown;
            OnSpellCooldownChange(this);
        }

        public StrugglerSpellWrapper(Spell x)
        {
            spell = x;
            isOnCooldown = false;
            cooldownTimer = x.Cooldown;
            OnSpellCooldownChange(this);
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(SpellCaster_Struggler))]
public class SpellCasterStrugglerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as SpellCaster_Struggler;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("InstantiatedSpellsParent"), true);
        serializedObject.ApplyModifiedProperties();
        if (myScript.gameObject.GetComponent<EffectsModule>())
        {
            GUILayout.Label("EffectsModule detected");
            if (EditorApplication.isPlaying)
            {
                GUILayout.Label("View only");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OwnedSpells"), true);
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }
        }
        else
        {
            GUILayout.Label("EffectsModule not detected");
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OwnedSpells"), true);
            serializedObject.ApplyModifiedProperties();
        }

        if (myScript.gameObject.GetComponent<SpellCaster>() != myScript)
        {
            Debug.LogError("Multiple SpellCaster component on " + myScript.gameObject.name);
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AnimationEvents"), true);
        serializedObject.ApplyModifiedProperties();

    }
}
#endif










