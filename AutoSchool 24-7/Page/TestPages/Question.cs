namespace AutoSchool_24_7.Page.TestPages
{
    public class Question
    {
        public string Text { get; }
        public string Image { get; }
        public string[] Options { get; }
        public int CorrectAnswerIndex { get; }

        public string[] ShuffledOptions { get; private set; }

        public Question(string text, string image, string[] options, int correctAnswerIndex)
        {
            Text = text;
            Image = image;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
            ShuffleOptions();
        }

        public void ShuffleOptions()
        {
            var shuffledList = Options.ToList();
            shuffledList = shuffledList.OrderBy(x => Guid.NewGuid()).ToList(); 
            ShuffledOptions = shuffledList.ToArray();
        }
    }
}

