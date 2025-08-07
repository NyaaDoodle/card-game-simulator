using Mirror;

public class GameInstance : NetworkBehaviour
{
    [SyncVar] private Table table;
    private readonly SyncList<Deck> decks = new SyncList<Deck>();
    private readonly SyncList<Space> spaces = new SyncList<Space>();
    private readonly SyncList<Player> players = new SyncList<Player>();

    public override void OnStartClient()
    {
        decks.OnAdd += OnDecksItemAdded;
        decks.OnInsert += OnDecksItemInserted;
        decks.OnSet += OnDecksItemChanged;
        decks.OnRemove += OnDecksItemRemoved;
        decks.OnClear += OnDecksListCleared;

        spaces.OnAdd += OnSpacesItemAdded;
        spaces.OnInsert += OnSpacesItemInserted;
        spaces.OnSet += OnSpacesItemChanged;
        spaces.OnRemove += OnSpacesItemRemoved;
        spaces.OnClear += OnSpacesListCleared;

        players.OnAdd += OnPlayersItemAdded;
        players.OnInsert += OnPlayersItemInserted;
        players.OnSet += OnPlayersItemChanged;
        players.OnRemove += OnPlayersItemRemoved;
        players.OnClear += OnPlayersListCleared;
    }

    public override void OnStopClient()
    {
        decks.OnAdd -= OnDecksItemAdded;
        decks.OnInsert -= OnDecksItemInserted;
        decks.OnSet -= OnDecksItemChanged;
        decks.OnRemove -= OnDecksItemRemoved;
        decks.OnClear -= OnDecksListCleared;

        spaces.OnAdd -= OnSpacesItemAdded;
        spaces.OnInsert -= OnSpacesItemInserted;
        spaces.OnSet -= OnSpacesItemChanged;
        spaces.OnRemove -= OnSpacesItemRemoved;
        spaces.OnClear -= OnSpacesListCleared;

        players.OnAdd -= OnPlayersItemAdded;
        players.OnInsert -= OnPlayersItemInserted;
        players.OnSet -= OnPlayersItemChanged;
        players.OnRemove -= OnPlayersItemRemoved;
        players.OnClear -= OnPlayersListCleared;
    }

    private void OnPlayersListCleared()
    {
        throw new System.NotImplementedException();
    }

    private void OnPlayersItemRemoved(int i_Arg1, Player i_Arg2)
    {
        throw new System.NotImplementedException();
    }

    private void OnPlayersItemChanged(int i_Arg1, Player i_Arg2)
    {
        throw new System.NotImplementedException();
    }

    private void OnPlayersItemInserted(int i_Obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnPlayersItemAdded(int i_Obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnSpacesListCleared()
    {
        throw new System.NotImplementedException();
    }

    private void OnSpacesItemRemoved(int i_Arg1, Space i_Arg2)
    {
        throw new System.NotImplementedException();
    }

    private void OnSpacesItemChanged(int i_Arg1, Space i_Arg2)
    {
        throw new System.NotImplementedException();
    }

    private void OnSpacesItemInserted(int i_Obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnSpacesItemAdded(int i_Obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnDecksListCleared()
    {
        throw new System.NotImplementedException();
    }

    private void OnDecksItemRemoved(int i_Arg1, Deck i_Arg2)
    {
        throw new System.NotImplementedException();
    }

    private void OnDecksItemChanged(int i_Arg1, Deck i_Arg2)
    {
        throw new System.NotImplementedException();
    }

    private void OnDecksItemInserted(int i_Obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnDecksItemAdded(int i_Obj)
    {
        throw new System.NotImplementedException();
    }

    [Server]
    public void Setup(GameTemplate gameTemplate)
    {

    }
}
