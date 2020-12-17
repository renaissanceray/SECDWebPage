using System;
using System.Collections.Generic;
using System.Text;

namespace ConvertSheetToPDF.Data
{
    public class Module
    {
        public Header Header { get; set; }
        public List<Section> Sections { get; set; } = new List<Section>();
        public Dialogue BeginningDialogue { get; set; }
        public Assessment BeginningDialogueAssessment { get; set; }
        public Dialogue EndingDialogue { get; set; }
        public Assessment EndingDialogueAssessment { get; set; }
        public Footer Footer { get; set; }
    }
    public class Header
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TemplateTabName { get; set; }
        public bool Display { get; set; }
        public string ModuleType { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string ModuleTeam { get; set; }
        public string ModuleAuthors { get; set; }
        public string ModuleDuration { get; set; }
        public string ModuleDueDate { get; set; }
        public string ModuleObjectives { get; set; }
        public string WhenToUse { get; set; }
        public string WhyItWorks { get; set; }
        public List<Term> Terms { get; set; } = new List<Term>();

    }
    public class Term
    {
        public string Text { get; set; }
        public string Definition { get; set; }
    }
    public class Footer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TemplateTabName { get; set; }
        public bool Display { get; set; }
        public List<Footnote> Footnotes { get; set; } = new List<Footnote>();
        public string HelpDeskText { get; set; }
        public string HelpDeskLink { get; set; }
        public string DonationText { get; set; }
        public string DonationImage { get; set; }
        public string DonationLink { get; set; }
        public string CopyrightText { get; set; }


    }
    public class Footnote
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }
    public class Section
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TemplateTabName { get; set; }
        public bool Display { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public List<Paragraph> Paragraphs { get; set; } = new List<Paragraph>();
        public Dialogue Dialogue { get; set; }
        public Assessment Assessment { get; set; }
        public List<Reference> References { get; set; } = new List<Reference>();
    }
    public class Paragraph {
        public string Image { get; set; }
        public string Text { get; set; }

    }

    public class Dialogue {
        public string Name { get; set; }
        public bool Display { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public string Setup { get; set; }
        public List<string> Lines { get; set; } = new List<string>();
    }
    public class Assessment {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
    public class Question
    {
        public Guid ID = Guid.NewGuid();
        public string Text { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
    public class Answer
    {
        public Guid ID = Guid.NewGuid();
        public string Text { get; set; }
        public string Detail { get; set; }
    }
    public class Reference {
        public string Name { get; set; }
        public bool Display { get; set; }
        public string Description { get; set; }
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
    public class Resource
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }
}
