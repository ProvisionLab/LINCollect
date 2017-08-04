package ua.com.lsoft.lincollect.activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashSet;

import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.db.SurveyDBHelper;
import ua.com.lsoft.lincollect.fragments.PageFragment;
import ua.com.lsoft.lincollect.model.main.Company;
import ua.com.lsoft.lincollect.model.main.Relationship;
import ua.com.lsoft.lincollect.utils.MethodUtils;
import ua.com.lsoft.lincollect.utils.SharedPrefsUtil;

import static ua.com.lsoft.lincollect.fragments.SecondGroupQuestionFragment.COMPANY_NAME;

public class AddCompanyActivity extends AppCompatActivity {

    private EditText companyNameEditText;
    private SurveyDBHelper surveyDBHelper;
    private Relationship relationship;
    public static final String RELATION_NODES = "relation_nodes";
    public static final String COMPANY_LIST = "company_list";
    public static final String ADDED_COMPANIES = "added_companies";
    private ArrayList<String> companies;

    private static final String TAG = AddCompanyActivity.class.getSimpleName();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_company);

        companies = (ArrayList<String>) getIntent().getSerializableExtra(COMPANY_LIST);
        surveyDBHelper = SurveyDBHelper.getInstance(this);
        relationship = (Relationship) getIntent().getSerializableExtra(RELATION_NODES);
        HashSet<String> addedCompanies = (HashSet<String>) SharedPrefsUtil.getStringSet(this, ADDED_COMPANIES);
        for (String company : addedCompanies) {
            companies.add(company.toLowerCase());
        }
        Log.d(TAG, "Added Companies " + companies);

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayShowTitleEnabled(false);
        TextView titleTextView = (TextView) toolbar.findViewById(R.id.toolbar_title);
        titleTextView.setText(R.string.add_company_title);

        ScrollView scrollView = (ScrollView) findViewById(R.id.addCompanyLayout);
        MethodUtils.hideKeyboardOnTouch(scrollView, this);

        companyNameEditText = (EditText) findViewById(R.id.company_name);
        final PageFragment pageFragment = PageFragment.newInstance(relationship, -1);
        getFragmentManager().beginTransaction().add(R.id.fragment_layout, pageFragment).commit();

        findViewById(R.id.btnSave).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (companyNameEditText.getText().toString().isEmpty()) {
                    Toast.makeText(AddCompanyActivity.this, getString(R.string.fill_out_company_error), Toast.LENGTH_SHORT).show();
                } else if (companies.contains(companyNameEditText.getText().toString().toLowerCase())) {
                    Toast.makeText(AddCompanyActivity.this, getString(R.string.company_exists), Toast.LENGTH_SHORT).show();
                } else {
                    ArrayList<Boolean> isAllValidList = pageFragment.validation(pageFragment.getViewModels());
                    if (!isAllValidList.contains(false)) {
                        Toast.makeText(AddCompanyActivity.this, getString(R.string.add_company_thanks), Toast.LENGTH_SHORT).show();

                        Company company = new Company(companyNameEditText.getText().toString(), true);
                        pageFragment.getRelationshipAnswersFromModels(relationship.getId(), pageFragment.getViewModels(), "node", companyNameEditText.getText().toString());

                        Intent result = new Intent();
                        result.putExtra(COMPANY_NAME, company);
                        setResult(RESULT_OK, result);

                        HashSet<String> set = (HashSet<String>) SharedPrefsUtil.getStringSet(AddCompanyActivity.this, ADDED_COMPANIES);
                        set.add(company.getName());
                        SharedPrefsUtil.putStringSet(AddCompanyActivity.this, ADDED_COMPANIES, set);
                        finish();
                    } else {
                        Toast.makeText(AddCompanyActivity.this, getString(R.string.answer_mandatory), Toast.LENGTH_LONG).show();
                    }
                }
            }
        });
    }
}
