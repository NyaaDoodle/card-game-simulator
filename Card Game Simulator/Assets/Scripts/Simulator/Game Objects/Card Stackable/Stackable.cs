using System;
using System.Text;

public class Stackable : CardCollection
{
    public IStackableData StackableData { get; private set; }
    public Card TopCard => FirstCard;

    // Events
    public event Action<Stackable> CardsShuffled;

    public override void OnStartClient()
    {
        base.OnStartClient();
        attachToTableObjectsContainer();
    }

    private void attachToTableObjectsContainer()
    {
        gameObject.transform.SetParent(ContainerReferences.Instance.TableObjectsContainer, false);
    }

    public virtual void Setup(IStackableData stackableData)
    {
        StackableData = stackableData;
    }

    public override void AddCard(Card card, int index)
    {
        card = card.FaceDown();
        base.AddCard(card, index);
    }

    public void Shuffle()
    {
        if (Cards.Count <= 1) return;

        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (Cards[i], Cards[randomIndex]) = (Cards[randomIndex], Cards[i]);
        }

        OnCardsShuffled();
    }

    protected virtual void OnCardsShuffled()
    {
        CardsShuffled?.Invoke(this);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"StackableData {StackableData.ToString()}");
        stringBuilder.AppendLine(base.ToString());
        return stringBuilder.ToString();
    }
}
