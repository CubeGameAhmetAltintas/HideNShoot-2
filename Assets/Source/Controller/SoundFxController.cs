using System.Collections.Generic;
using UnityEngine;

public class SoundFxController : ControllerBaseModel
{
    public static SoundFxController Controller;
    [SerializeField] List<AudioModel> clappingFx;
    [SerializeField] AudioModel scissorsFx;
    [SerializeField] AudioModel sewingMachineFx;
    [SerializeField] AudioModel printMachineFx;
    [SerializeField] AudioModel fabricRoll;
    [SerializeField] AudioModel footStep;
    [SerializeField] AudioModel fashionStageMusic;
    public float MaxVolume;
    public float Volume;

    public override void Initialize()
    {
        base.Initialize();
        Controller = this;
        //Volume = DataManager.Settings.SoundsFXLevel;
    }

    public void SetVolume(float volume)
    {
        volume = volume < MaxVolume ? volume : MaxVolume;
        Volume = volume;
    }

    public void PlayClappingSfx()
    {
        if (Volume > 0)
            clappingFx.GetRandom().PlaySound(Volume);
    }

    public void PlayScissorsSfx()
    {
        if (Volume > 0)
            scissorsFx.PlaySound(Volume, true, 1, 1.5f);
    }

    public void StopScissorsSfx()
    {
        if (Volume > 0)
            scissorsFx.Stop();
    }

    public void PlaySewingMachineSfx()
    {
        if (Volume > 0)
            sewingMachineFx.PlayLoopSound(Volume);

    }

    public void StopSewingMachineSfx()
    {
        if (Volume > 0)
            sewingMachineFx.Stop();
    }

    public void PlayPrintMachineSfx()
    {
        if (Volume > 0)
            printMachineFx.PlayLoopSound(Volume);
    }

    public void StopPrintMachineSfx()
    {
        if (Volume > 0)
            printMachineFx.Stop();
    }

    public void PlayFabricRollSfx()
    {
        fabricRoll.PlaySound(Volume);
    }

    public void PlayFootStep()
    {
        footStep.PlaySound(Volume,true, 0.75f,1.25f);
    }


    public void PlayFashionStageMusic()
    {
        fashionStageMusic.PlaySound(Volume * 0.5f);
    }

}
