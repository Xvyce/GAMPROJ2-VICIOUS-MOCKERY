using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page 
{
    public string Title { get; set; }
    public string Text { get; set; }
    public string Summary { get; set; }
    public List<string> Pages { get; set; }

    private static List<Page> _pageList = null;

    public static Page RandomPage;

    public static int CurrentPage1 = 0;
    public static int CurrentPage2 = 1;
    public static int CurrentPage3 = 2;
    public static int CurrentPage4 = 3;

   
    public static Page GetRandomPage()
    {
        List<Page> pageList = Page.PageList;

        //int num = UnityEngine.Random.Range(0, pageList.Count);
        int num = 0;
        Page pge = pageList[num];
        pge.Pages = new List<string>();

        string[] words = pge.Text.Split(' ');
        //put 7 words on each page
        string page = "";
        int wordCnt = 0;

       foreach (string word in words)
        {
            wordCnt++;
            if(wordCnt > 100)
            {
                pge.Pages.Add(page);
                page = "";
                wordCnt = 0;
            }
            page += string.Format("{0} ", word);
        }
        pge.Pages.Add(page);
        RandomPage = pge;

        return pge;
    }

    public static List<Page> PageList
    {
        get
        {
            if (_pageList == null)
            {
                _pageList = new List<Page>();

                _pageList.Add(new Page
                { // 1
                    Title = "TUTORIAL",
                    Summary = "Level Story Summary",
                    Text = "The bard has been drinking his sorrows away in a bar as he was kicked out of his party. " +
                    "Now he’s too drunk and hallucinating other humans as goblins and orcs. " +
                    "As a bard, his only way of defence is to shoo these monsters away with his terrible words. " +
                    "Now he’s hurting the people and causing a ruckus inside the bar. " +
                    "Will the bard snap out of it or would he continue his drunken ordeal. "




                }) ;

                _pageList.Add(new Page
                { // 2
                    Title = "II",
                    Summary= "Level Story Summary",
                    Text = "Level 2"

                });

                _pageList.Add(new Page
                { // 3
                    Title = "III",
                    Summary = "Level Story Summary",
                    Text = "Level 3"

                });


            }
            return _pageList;
        }
    }

}
