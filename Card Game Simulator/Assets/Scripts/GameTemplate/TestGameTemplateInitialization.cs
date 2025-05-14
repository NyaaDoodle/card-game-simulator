using System.Collections.Generic;
using UnityEngine;

public class TestGameTemplateInitialization
{
    public GameTemplate GetTestTemplate()
    {
        const string tableSurfaceImageRelativePath = "Standard52/Back_cards-07";
        const string cardBackSideImageRelativePath = "Standard52/Gray_back";
        return new GameTemplate()
                   {
                       Id = 1,
                       Name = "Test Game Template",
                       TableData =
                           new TableData()
                               {
                                   Width = 7f,
                                   Height = 5f,
                                   SurfaceImage = Resources.Load<Sprite>(tableSurfaceImageRelativePath)
                               },
                       CardPool = new Dictionary<int, CardData>()
                                      {
                                          {
                                              1,
                                              new CardData()
                                                  {
                                                      Id = 1,
                                                      FrontSideSprite = Resources.Load<Sprite>("Standard52/2C"),
                                                      BackSideSprite =
                                                          Resources.Load<Sprite>(cardBackSideImageRelativePath)
                                                  }
                                          },
                                          {
                                              2,
                                              new CardData()
                                                  {
                                                      Id = 2,
                                                      FrontSideSprite = Resources.Load<Sprite>("Standard52/3S"),
                                                      BackSideSprite =
                                                          Resources.Load<Sprite>(cardBackSideImageRelativePath)
                                                  }
                                          },
                                          {
                                              3,
                                              new CardData()
                                                  {
                                                      Id = 3,
                                                      FrontSideSprite = Resources.Load<Sprite>("Standard52/4H"),
                                                      BackSideSprite =
                                                          Resources.Load<Sprite>(cardBackSideImageRelativePath)
                                                  }
                                          },
                                          {
                                              4,
                                              new CardData()
                                                  {
                                                      Id = 4,
                                                      FrontSideSprite = Resources.Load<Sprite>("Standard52/5D"),
                                                      BackSideSprite =
                                                          Resources.Load<Sprite>(cardBackSideImageRelativePath)
                                                  }
                                          },
                                          {
                                              5,
                                              new CardData()
                                                  {
                                                      Id = 5,
                                                      FrontSideSprite = Resources.Load<Sprite>("Standard52/6C"),
                                                      BackSideSprite =
                                                          Resources.Load<Sprite>(cardBackSideImageRelativePath)
                                                  }
                                          },
                                          {
                                              6,
                                              new CardData()
                                                  {
                                                      Id = 6,
                                                      FrontSideSprite = Resources.Load<Sprite>("Standard52/7D"),
                                                      BackSideSprite =
                                                          Resources.Load<Sprite>(cardBackSideImageRelativePath)
                                                  }
                                          }
                                      },
                       DecksData = new Dictionary<int, DeckData>() { { 1, new DeckData() { Id = 1, CardIds = null } } },
                       PlacementLocationDictionary = null
                   };
    }
}