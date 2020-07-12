public class ClothStateInterval : BaseSingleton<ClothStateInterval>
{
    private float wet = 4;
    private float mostlyWet = 3;
    private float mostlyDry = 2;
    private float dry = 1;

    public float GetInterval(ClothState state)
    {
        switch (state)
        {
            case ClothState.Wet: return wet;
            case ClothState.MostlyWet: return mostlyWet;
            case ClothState.MostlyDry: return mostlyDry;
            case ClothState.Dry: return dry;
            default: return 0;
        }
    }
}