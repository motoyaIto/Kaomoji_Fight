/// <summary>
/// レイヤー名を定数で管理するクラス
/// </summary>
public static class LayerName
{
	public const int Default = 0;
	public const int TransparentFX = 1;
	public const int IgnoreRaycast = 2;
	public const int Water = 4;
	public const int UI = 5;
	public const int Obstacle = 8;
	public const int Player = 9;
	public const int BG = 10;
	public const int Through = 11;
	public const int Stage = 12;
	public const int Weapon = 13;
	public const int DefaultMask = 1;
	public const int TransparentFXMask = 2;
	public const int IgnoreRaycastMask = 4;
	public const int WaterMask = 16;
	public const int UIMask = 32;
	public const int ObstacleMask = 256;
	public const int PlayerMask = 512;
	public const int BGMask = 1024;
	public const int ThroughMask = 2048;
	public const int StageMask = 4096;
	public const int WeaponMask = 8192;
}
