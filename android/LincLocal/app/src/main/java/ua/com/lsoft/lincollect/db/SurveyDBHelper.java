package ua.com.lsoft.lincollect.db;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import java.util.ArrayList;

import ua.com.lsoft.lincollect.db.SurveyContract.AnswerModelEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.CompanyEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.NodeValueEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.QuestionAnswerEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.RelationshipEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.RelationshipNodeQuestionAnswerEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.RelationshipQuestionAnswerEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.SectionEntry;
import ua.com.lsoft.lincollect.db.SurveyContract.ValueEntry;
import ua.com.lsoft.lincollect.model.main.Companies;
import ua.com.lsoft.lincollect.model.main.Company;
import ua.com.lsoft.lincollect.model.main.NodeValue;
import ua.com.lsoft.lincollect.model.main.RelationshipAnswer;
import ua.com.lsoft.lincollect.model.main.RelationshipNodeQuestionAnswer;
import ua.com.lsoft.lincollect.model.main.RelationshipQuestionAnswer;
import ua.com.lsoft.lincollect.model.main.Value;

/**
 * Created by Evgeniy on 10.05.2016.
 */
public class SurveyDBHelper extends SQLiteOpenHelper {

    private static SurveyDBHelper helperInstance;
    public static final int DATABASE_VERSION = 1;
    public static final String DATABASE_NAME = "surveys.db";
    private static final String TAG = SurveyDBHelper.class.getSimpleName();

    private static final String CREATE_TABLE_ANSWER_MODEL = "CREATE TABLE " +
            AnswerModelEntry.TABLE_NAME + "(" +
            AnswerModelEntry.COLUMN_USER_LINK_ID + " INTEGER PRIMARY KEY," +
            AnswerModelEntry.COLUMN_BEFORE_ID + " INTEGER," +
            AnswerModelEntry.COLUMN_AFTER_ID + " INTEGER," +
            AnswerModelEntry.COLUMN_RELATIONSHIP_ID + " INTEGER" + ")";

    private static final String CREATE_TABLE_SECTION = "CREATE TABLE " +
            SectionEntry.TABLE_NAME + "(" +
            SectionEntry.COLUMN_ID + " INTEGER PRIMARY KEY," +
            SectionEntry.COLUMN_QUESTION_ANSWER_ID + " INTEGER" + ")";

    private static final String CREATE_TABLE_QUESTION_ANSWER = "CREATE TABLE " +
            QuestionAnswerEntry.TABLE_NAME + "(" +
            QuestionAnswerEntry.COLUMN_ID + " INTEGER PRIMARY KEY," +
            QuestionAnswerEntry.COLUMN_VALUE_ID + " INTEGER" + ")";

    private static final String CREATE_TABLE_NODE_VALUE = "CREATE TABLE " +
            NodeValueEntry.TABLE_NAME + "(" +
            NodeValueEntry.COLUMN_QUESTION_ID + " INTEGER," +
            NodeValueEntry.COLUMN_COMPANY + " TEXT," +
            NodeValueEntry.COLUMN_VALUE + " TEXT" + ")";

    private static final String CREATE_TABLE_VALUE = "CREATE TABLE " +
            ValueEntry.TABLE_NAME + "(" +
            ValueEntry.COLUMN_QUESTION_ID + " INTEGER," +
            ValueEntry.COLUMN_VALUE + " TEXT" + ")";

    private static final String CREATE_TABLE_RELATIONSHIP = "CREATE TABLE " +
            RelationshipEntry.TABLE_NAME + "(" +
            RelationshipEntry.COLUMN_ID + " INTEGER PRIMARY KEY" + ")";

    private static final String CREATE_TABLE_RELATIONSHIP_QUESTION_ANSWER = "CREATE TABLE " +
            RelationshipQuestionAnswerEntry.TABLE_NAME + "(" +
            RelationshipQuestionAnswerEntry.COLUMN_ID + " INTEGER," +
            RelationshipQuestionAnswerEntry.COLUMN_RELATIONSHIP_ID + " INTEGER" + ")";

    private static final String CREATE_TABLE_RELATIONSHIP_NODE_QUESTION_ANSWER = "CREATE TABLE " +
            RelationshipNodeQuestionAnswerEntry.TABLE_NAME + "(" +
            RelationshipNodeQuestionAnswerEntry.COLUMN_ID + " INTEGER," +
            RelationshipNodeQuestionAnswerEntry.COLUMN_RELATIONSHIP_ID + " INTEGER," +
            RelationshipNodeQuestionAnswerEntry.COLUMN_COMPANY + " TEXT" + ")";

    private static final String CREATE_TABLE_COMPANY = "CREATE TABLE " +
            CompanyEntry.TABLE_NAME + "(" +
            CompanyEntry.COLUMN_RELATIONSHIP_ID + " INTEGER," +
            CompanyEntry.COLUMN_NAME + " TEXT" + ")";

    private static final String DROP_TABLE_ANSWER_MODEL =
            "DROP TABLE IF EXISTS " + AnswerModelEntry.TABLE_NAME;

    private static final String DROP_TABLE_SECTION =
            "DROP TABLE IF EXISTS " + SectionEntry.TABLE_NAME;

    private static final String DROP_TABLE_QUESTION_ANSWER =
            "DROP TABLE IF EXISTS " + QuestionAnswerEntry.TABLE_NAME;

    private static final String DROP_TABLE_VALUE =
            "DROP TABLE IF EXISTS " + ValueEntry.TABLE_NAME;

    private static final String DROP_TABLE_NODE_VALUE =
            "DROP TABLE IF EXISTS " + NodeValueEntry.TABLE_NAME;

    private static final String DROP_TABLE_RELATIONSHIP =
            "DROP TABLE IF EXISTS " + RelationshipEntry.TABLE_NAME;

    private static final String DROP_TABLE_RELATIONSHIP_QUESTION_ANSWER =
            "DROP TABLE IF EXISTS " + RelationshipQuestionAnswerEntry.TABLE_NAME;

    private static final String DROP_TABLE_RELATIONSHIP_NODE_QUESTION_ANSWER =
            "DROP TABLE IF EXISTS " + RelationshipNodeQuestionAnswerEntry.TABLE_NAME;

    private static final String DROP_TABLE_COMPANY =
            "DROP TABLE IF EXISTS " + CompanyEntry.TABLE_NAME;

    private Context context;

    public static SurveyDBHelper getInstance(Context context) {

        if (helperInstance == null) {
            helperInstance = new SurveyDBHelper(context.getApplicationContext());
        }
        return helperInstance;
    }

    private SurveyDBHelper(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        this.context = context;
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
//        db.execSQL(CREATE_TABLE_ANSWER_MODEL);
//        db.execSQL(CREATE_TABLE_SECTION);
//        db.execSQL(CREATE_TABLE_QUESTION_ANSWER);
        db.execSQL(CREATE_TABLE_VALUE);
        db.execSQL(CREATE_TABLE_NODE_VALUE);
        db.execSQL(CREATE_TABLE_COMPANY);
        db.execSQL(CREATE_TABLE_RELATIONSHIP);
        db.execSQL(CREATE_TABLE_RELATIONSHIP_QUESTION_ANSWER);
        db.execSQL(CREATE_TABLE_RELATIONSHIP_NODE_QUESTION_ANSWER);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
//        db.execSQL(DROP_TABLE_ANSWER_MODEL);
//        db.execSQL(DROP_TABLE_SECTION);
//        db.execSQL(DROP_TABLE_QUESTION_ANSWER);
        db.execSQL(DROP_TABLE_VALUE);
        db.execSQL(DROP_TABLE_NODE_VALUE);
        db.execSQL(DROP_TABLE_COMPANY);
        db.execSQL(DROP_TABLE_RELATIONSHIP);
        db.execSQL(DROP_TABLE_RELATIONSHIP_QUESTION_ANSWER);
        db.execSQL(DROP_TABLE_RELATIONSHIP_NODE_QUESTION_ANSWER);

        onCreate(db);
    }

    @Override
    public void onDowngrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        onUpgrade(db, oldVersion, newVersion);
    }

    public long insertRelationship(RelationshipAnswer relationship) {
        SQLiteDatabase db = getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(RelationshipEntry.COLUMN_ID, relationship.getId());

        return db.insert(RelationshipEntry.TABLE_NAME, null, values);
    }

    public long insertRelationshipQuestionAnswer(RelationshipQuestionAnswer questionAnswer) {
        SQLiteDatabase db = getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(RelationshipQuestionAnswerEntry.COLUMN_ID, questionAnswer.getQuestionId());
        values.put(RelationshipQuestionAnswerEntry.COLUMN_RELATIONSHIP_ID, questionAnswer.getRelationshipId());

        for (String value : questionAnswer.getValues()) {
            insertValue(new Value(questionAnswer.getQuestionId(), value));
        }

        return db.insert(RelationshipQuestionAnswerEntry.TABLE_NAME, null, values);
    }

    public long insertRelationshipNodeQuestionAnswer(RelationshipNodeQuestionAnswer questionAnswer) {
        SQLiteDatabase db = getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(RelationshipNodeQuestionAnswerEntry.COLUMN_ID, questionAnswer.getQuestionId());
        values.put(RelationshipNodeQuestionAnswerEntry.COLUMN_RELATIONSHIP_ID, questionAnswer.getRelationshipId());
        values.put(RelationshipNodeQuestionAnswerEntry.COLUMN_COMPANY, questionAnswer.getCompanyName());

        for (String value : questionAnswer.getValues()) {
            insertNodeValue(new NodeValue(questionAnswer.getQuestionId(), value, questionAnswer.getCompanyName()));
        }

        return db.insert(RelationshipNodeQuestionAnswerEntry.TABLE_NAME, null, values);
    }

    public long insertCompany(Company company) {
        SQLiteDatabase db = getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(CompanyEntry.COLUMN_RELATIONSHIP_ID, company.getRelationshipId());
        values.put(CompanyEntry.COLUMN_NAME, company.getName());

        return db.insert(CompanyEntry.TABLE_NAME, null, values);
    }

    public long insertValue(Value value) {
        SQLiteDatabase db = getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(ValueEntry.COLUMN_QUESTION_ID, value.getQuestionId());
        values.put(ValueEntry.COLUMN_VALUE, value.getValue());

        return db.insert(ValueEntry.TABLE_NAME, null, values);
    }

    public long insertNodeValue(NodeValue value) {
        SQLiteDatabase db = getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(NodeValueEntry.COLUMN_QUESTION_ID, value.getQuestionId());
        values.put(NodeValueEntry.COLUMN_VALUE, value.getValue());
        values.put(NodeValueEntry.COLUMN_COMPANY, value.getCompany());

        return db.insert(NodeValueEntry.TABLE_NAME, null, values);
    }

    public ArrayList<RelationshipAnswer> getRelationshipAnswers() {
        ArrayList<RelationshipAnswer> relationshipAnswers = new ArrayList<>();

        String selectQuery = "SELECT  * FROM " + RelationshipEntry.TABLE_NAME;

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                RelationshipAnswer relationshipAnswer = new RelationshipAnswer();
                long id = Long.parseLong(cursor.getString(0));
                relationshipAnswer.setId(id);
                relationshipAnswer.setCompanies(new Companies(getAllCompaniesByRelationId(id)));
                relationshipAnswer.setAnswers(getAllRelationshipAnswers(id));
                relationshipAnswer.setNodeAnswers(getAllRelationshipNodeAnswers(id));
                relationshipAnswers.add(relationshipAnswer);
            } while (cursor.moveToNext());
        }

        cursor.close();

        return relationshipAnswers;
    }

    public ArrayList<RelationshipQuestionAnswer> getAllRelationshipAnswers(long relationId) {
        ArrayList<RelationshipQuestionAnswer> answers = new ArrayList<>();

        String selectQuery = "SELECT  * FROM " + RelationshipQuestionAnswerEntry.TABLE_NAME + " WHERE "
                + RelationshipQuestionAnswerEntry.COLUMN_RELATIONSHIP_ID + " = " + relationId;

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                RelationshipQuestionAnswer answer = new RelationshipQuestionAnswer();
                long id = Long.parseLong(cursor.getString(0));
                answer.setQuestionId(id);
                answer.setRelationshipId(Long.parseLong(cursor.getString(1)));
                answer.setValues(getAllValuesByQuestionId(id));
                answers.add(answer);
            } while (cursor.moveToNext());
        }

        cursor.close();

        return answers;
    }

    public ArrayList<RelationshipNodeQuestionAnswer> getAllRelationshipNodeAnswers(long relationId) {
        ArrayList<RelationshipNodeQuestionAnswer> answers = new ArrayList<>();

        String selectQuery = "SELECT  * FROM " + RelationshipNodeQuestionAnswerEntry.TABLE_NAME + " WHERE "
                + RelationshipNodeQuestionAnswerEntry.COLUMN_RELATIONSHIP_ID + " = " + relationId;

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                RelationshipNodeQuestionAnswer answer = new RelationshipNodeQuestionAnswer();
                long id = Long.parseLong(cursor.getString(0));
                answer.setQuestionId(id);
                answer.setRelationshipId(Long.parseLong(cursor.getString(1)));
                answer.setCompanyName(cursor.getString(2));
                answer.setValues(getAllNodeValuesByQuestionId(id, cursor.getString(2)));
                answers.add(answer);
            } while (cursor.moveToNext());
        }

        cursor.close();

        return answers;
    }

    public ArrayList<String> getAllValuesByQuestionId(long id) {
        ArrayList<String> values = new ArrayList<>();

        String selectQuery = "SELECT  * FROM " + ValueEntry.TABLE_NAME + " WHERE "
                + ValueEntry.COLUMN_QUESTION_ID + " = " + id;

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                String value = cursor.getString(1);
                values.add(value);
            } while (cursor.moveToNext());
        }

        cursor.close();

        return values;
    }

    public ArrayList<String> getAllNodeValuesByQuestionId(long id, String companyName) {
        ArrayList<String> values = new ArrayList<>();

        String selectQuery = "SELECT  * FROM " + NodeValueEntry.TABLE_NAME + " WHERE "
                + NodeValueEntry.COLUMN_QUESTION_ID + " = " + id + " AND " + NodeValueEntry.COLUMN_COMPANY + " = '" + companyName + "'";

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                String value = cursor.getString(2);
                values.add(value);
            } while (cursor.moveToNext());
        }

        cursor.close();

        return values;
    }

    public ArrayList<Company> getAllCompaniesByRelationId(long id) {
        ArrayList<Company> companies = new ArrayList<>();

        String selectQuery = "SELECT  * FROM " + CompanyEntry.TABLE_NAME + " WHERE "
                + CompanyEntry.COLUMN_RELATIONSHIP_ID + " = " + id;

        SQLiteDatabase db = getReadableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);

        if (cursor.moveToFirst()) {
            do {
                Company company = new Company();
                company.setRelationshipId(Long.parseLong(cursor.getString(0)));
                company.setName(cursor.getString(1));
                company.setChecked(true);
                companies.add(company);
            } while (cursor.moveToNext());
        }

        cursor.close();

        return companies;
    }

    public void clearAllTables() {
        Log.d(TAG, "Clear all tables");
        SQLiteDatabase db = getWritableDatabase();
        db.beginTransaction();
        try {
            db.execSQL("DELETE FROM " + RelationshipEntry.TABLE_NAME);
            db.execSQL("DELETE FROM " + RelationshipQuestionAnswerEntry.TABLE_NAME);
            db.execSQL("DELETE FROM " + RelationshipNodeQuestionAnswerEntry.TABLE_NAME);
            db.execSQL("DELETE FROM " + CompanyEntry.TABLE_NAME);
            db.execSQL("DELETE FROM " + ValueEntry.TABLE_NAME);
            db.execSQL("DELETE FROM " + NodeValueEntry.TABLE_NAME);
            db.setTransactionSuccessful();
        } finally {
            db.endTransaction();
        }
    }
}
