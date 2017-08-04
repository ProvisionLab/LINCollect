package ua.com.lsoft.lincollect.activity;

import android.app.Fragment;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.TextView;

import java.util.ArrayList;

import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.db.SurveyDBHelper;
import ua.com.lsoft.lincollect.fragments.PageFragment;
import ua.com.lsoft.lincollect.fragments.SecondGroupQuestionFragment;
import ua.com.lsoft.lincollect.model.main.PassSurvey;
import ua.com.lsoft.lincollect.utils.MethodUtils;

import static ua.com.lsoft.lincollect.activity.StartActivity.SURVEY;

public class QuizActivity extends AppCompatActivity {

    private SurveyDBHelper surveyDBHelper;
    private PassSurvey survey;
    private ArrayList<Fragment> fragments;
    private FrameLayout fragmentLayout;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_quiz);

        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayShowTitleEnabled(false);

        fragmentLayout = (FrameLayout) findViewById(R.id.fragment_layout);
        MethodUtils.hideKeyboardOnTouch(fragmentLayout, this);

        survey = (PassSurvey) getIntent().getSerializableExtra(SURVEY);

        fragments = new ArrayList<>();
        fragments.add(PageFragment.newInstance(survey.getSectionBefore(), 0, "Before"));
        for (int i = 0; i < survey.getItems().size(); i++) {
            fragments.add(SecondGroupQuestionFragment.newInstance(survey.getItems().get(i).getCompanies().getCompanyNames(),
                    2 * i + 1, survey.getItems().get(i).getId(), i));
            fragments.add(PageFragment.newInstance(survey.getItems().get(i), 2 * i + 2));
        }
        fragments.add(PageFragment.newInstance(survey.getSectionAfter(), 2 * survey.getItems().size() + 1, "After"));

//        surveyDBHelper = SurveyDBHelper.getInstance(this);
//        Survey survey = surveyDBHelper.getSurvey(surveyId);

        getFragmentManager().beginTransaction().add(R.id.fragment_layout, fragments.get(0)).commit();

        TextView titleTextView = (TextView) toolbar.findViewById(R.id.toolbar_title);
        titleTextView.setText(survey.getSurveyName());
    }

    public ArrayList<Fragment> getFragments() {
        return fragments;
    }

    public void setFragments(ArrayList<Fragment> fragments) {
        this.fragments = fragments;
    }

    public PassSurvey getSurvey() {
        return survey;
    }

    public void setSurvey(PassSurvey survey) {
        this.survey = survey;
    }

    @Override
    public void onBackPressed() {
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
    }
}
