using UnityEngine;

[CreateAssetMenu(fileName = "NewStageInfoData", menuName = "Stage Info Data")]
public class StageInfoData : ScriptableObject
{
    public int ThreeStarScore;
    public int TwoStarScore;
    public int OneStarScore;

    public int OneStarReward;
    public int TwoStarReward;
    public int ThreeStarReward;

    public bool isClearStage;
    public int   myBestScore;
}