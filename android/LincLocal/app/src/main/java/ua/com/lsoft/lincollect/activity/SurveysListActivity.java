package ua.com.lsoft.lincollect.activity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.model.main.ResponseData;
import ua.com.lsoft.lincollect.model.main.Survey;
import ua.com.lsoft.lincollect.network.ApiService;
import ua.com.lsoft.lincollect.network.RetrofitApi;
import ua.com.lsoft.lincollect.utils.SharedPrefsUtil;

import static android.provider.Telephony.Carriers.BEARER;
import static ua.com.lsoft.lincollect.activity.LoginActivity.TOKEN;
import static ua.com.lsoft.lincollect.activity.StartActivity.SURVEY_ID;
import static ua.com.lsoft.lincollect.activity.StartActivity.SURVEY_NAME;

public class SurveysListActivity extends AppCompatActivity {

    private ArrayList<Survey> surveys;
    private ListView surveysListView;
    private SurveyAdapter adapter;
    //    private SurveyDBHelper surveyDBHelper;
    private static final String TAG = SurveysListActivity.class.getSimpleName();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_task_list);

//        surveyDBHelper = SurveyDBHelper.getInstance(this);

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayShowTitleEnabled(false);
        TextView titleTextView = (TextView) toolbar.findViewById(R.id.toolbar_title);
        titleTextView.setText(R.string.surveys_list_choose);

        surveysListView = (ListView) findViewById(R.id.surveys_list);

        ApiService apiService = RetrofitApi.getInstance().getApiService();
        Call<ResponseData<ArrayList<Survey>>> surveysCall = apiService.getSurveys(BEARER + " " +
                SharedPrefsUtil.getStringData(this, TOKEN));
        findViewById(R.id.progress_layout).setVisibility(View.VISIBLE);
        surveysCall.enqueue(new Callback<ResponseData<ArrayList<Survey>>>() {
            @Override
            public void onResponse(Call<ResponseData<ArrayList<Survey>>> call, Response<ResponseData<ArrayList<Survey>>> response) {
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
                if (response.isSuccessful()) {
                    ResponseData<ArrayList<Survey>> responseResult = response.body();
                    Log.d(TAG, "Surveys " + responseResult.getData());
                    surveys = responseResult.getData();

                    if (surveys != null && surveys.size() > 0) {
                        adapter = new SurveyAdapter(SurveysListActivity.this, R.layout.survey_list_item, surveys);
                        surveysListView.setAdapter(adapter);
                    }
                } else {
                    Log.d(TAG, "Response error " + response.raw().toString());
                }
            }

            @Override
            public void onFailure(Call<ResponseData<ArrayList<Survey>>> call, Throwable t) {
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
                Log.d(TAG, "Error " + t.getMessage());
            }
        });

//        ArrayList<Survey> surveys = surveyDBHelper.getAllSurveys();
//        Log.d("Surveys", "Surv " + surveys.toString());

        surveysListView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent intent = new Intent(SurveysListActivity.this, StartActivity.class);
                Survey survey = adapter.getItem(position);
                intent.putExtra(SURVEY_ID, survey.getId());
                intent.putExtra(SURVEY_NAME, survey.getName());
                startActivity(intent);
                finish();
            }
        });
    }

    private class SurveyAdapter extends ArrayAdapter<Survey> {

        SurveyAdapter(Context context, int resource, ArrayList<Survey> objects) {
            super(context, resource, objects);
        }

        @NonNull
        @Override
        public View getView(int position, View convertView, @NonNull ViewGroup parent) {

            Survey survey = getItem(position);

            if (convertView == null) {
                convertView = LayoutInflater.from(getContext()).inflate(R.layout.survey_list_item, parent, false);
            }

            TextView surveyTextView = (TextView) convertView.findViewById(R.id.survey_name);
            surveyTextView.setText(survey.getName());

            return convertView;
        }
    }
}
