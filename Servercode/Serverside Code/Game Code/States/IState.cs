public interface IState
{
    void Enter(GameRoom room);  
    void Execute(GameRoom room);
    void Exit(GameRoom room); 
}