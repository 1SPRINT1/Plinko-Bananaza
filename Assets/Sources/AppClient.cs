public class AppClient : ScreenStates<IScreenState> {

    public AppClient(IScreenState[] states) {
        states.Each(AddToStates);
    }
    
}
