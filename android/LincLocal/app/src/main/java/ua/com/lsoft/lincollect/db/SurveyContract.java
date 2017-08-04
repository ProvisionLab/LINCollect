package ua.com.lsoft.lincollect.db;

import android.provider.BaseColumns;

/**
 * Created by Evgeniy on 11.05.2016.
 */
public final class SurveyContract {

    public SurveyContract() {
    }

    public static abstract class AnswerModelEntry implements BaseColumns {

        public static final String TABLE_NAME = "answer_model";

        public static final String COLUMN_USER_LINK_ID = "userLinkId";
        public static final String COLUMN_BEFORE_ID = "beforeId";
        public static final String COLUMN_AFTER_ID = "afterId";
        public static final String COLUMN_RELATIONSHIP_ID = "relationship_id";
    }

    public static abstract class SectionEntry implements BaseColumns {

        public static final String TABLE_NAME = "section";

        public static final String COLUMN_ID = "id";
        public static final String COLUMN_QUESTION_ANSWER_ID = "question_answer_id";
    }

    public static abstract class QuestionAnswerEntry implements BaseColumns {

        public static final String TABLE_NAME = "question_answer";

        public static final String COLUMN_ID = "id";
        public static final String COLUMN_VALUE_ID = "value_id";
    }

    public static abstract class ValueEntry implements BaseColumns {

        public static final String TABLE_NAME = "answer_values";

        public static final String COLUMN_QUESTION_ID = "questionId";
        public static final String COLUMN_VALUE = "value";
    }

    public static abstract class NodeValueEntry implements BaseColumns {

        public static final String TABLE_NAME = "answer_node_values";

        public static final String COLUMN_QUESTION_ID = "questionId";
        public static final String COLUMN_VALUE = "value";
        public static final String COLUMN_COMPANY = "company";
    }

    public static abstract class RelationshipEntry implements BaseColumns {

        public static final String TABLE_NAME = "relationship";

        public static final String COLUMN_ID = "id";
    }

    public static abstract class RelationshipQuestionAnswerEntry implements BaseColumns {

        public static final String TABLE_NAME = "rel_question_answer";

        public static final String COLUMN_ID = "id";
        public static final String COLUMN_RELATIONSHIP_ID = "relationship_id";
    }

    public static abstract class RelationshipNodeQuestionAnswerEntry implements BaseColumns {

        public static final String TABLE_NAME = "rel_node_question_answer";

        public static final String COLUMN_ID = "id";
        public static final String COLUMN_RELATIONSHIP_ID = "relationship_id";
        public static final String COLUMN_COMPANY = "company";
    }

    public static abstract class CompanyEntry implements BaseColumns {

        public static final String TABLE_NAME = "company";

        public static final String COLUMN_RELATIONSHIP_ID = "relationship_id";
        public static final String COLUMN_NAME = "name";
    }

    public static abstract class AnswerEntry implements BaseColumns {
        public static final String TABLE_NAME = "answer";

        public static final String COLUMN_JSON_ANSWER = "json_answer";
    }
}
