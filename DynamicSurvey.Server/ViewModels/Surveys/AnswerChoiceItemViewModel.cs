﻿namespace DynamicSurvey.Server.ViewModels.Surveys
{
    public class AnswerChoiceItemViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Coding { get; set; }
        public bool IsDefault { get; set; }
    }
    public class MatrixRow
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class MatrixCol
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
