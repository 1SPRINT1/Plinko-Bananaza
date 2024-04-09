public class MenuButton : ButtonElement {
    protected override void OnButtonClick() {
        SystemPlayer.Play(SystemEffectType.ButtonClick);
    }
}