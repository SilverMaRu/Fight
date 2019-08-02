using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAttributesUIManager : MonoBehaviour
{
    public int relatedPlayerIndex = 1;
    public UpdateStrip HPUpdateStrip;
    public UpdateStrip MPUpdateStrip;
    public UpdateStrip SPUpdateStrip;
    private Player relatedPlayer;
    // 关联的FighterAttributesManager
    private FighterAttributesManager relatedAttrManager;

    //// Start is called before the first frame update
    void Start()
    {
        relatedPlayer = Player.GetPlayerByIndex(relatedPlayerIndex);
        GameObject fighterIns = SpawFighter.GetFighterIns(relatedPlayer);
        if (fighterIns != null)
        {
            relatedAttrManager = fighterIns.GetComponent<FighterAttributesManager>();
        }

        Transform myTrans = transform;
        if (HPUpdateStrip != null && HPUpdateStrip.gameObject.activeSelf)
        {
            HPUpdateStrip.fighterIns = fighterIns;
            relatedAttrManager.HPChangeEvent += HPUpdateStrip.OnChange;
        }
        if (MPUpdateStrip != null && MPUpdateStrip.gameObject.activeSelf)
        {
            MPUpdateStrip.fighterIns = fighterIns;
            relatedAttrManager.MPChangeEvent += MPUpdateStrip.OnChange;
        }
        if (SPUpdateStrip != null && SPUpdateStrip.gameObject.activeSelf)
        {
            SPUpdateStrip.fighterIns = fighterIns;
            relatedAttrManager.SPChangeEvent += SPUpdateStrip.OnChange;
        }
    }
}
