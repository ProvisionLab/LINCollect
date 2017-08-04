package ua.com.lsoft.lincollect.activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.EditText;
import android.widget.ScrollView;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.db.SurveyDBHelper;
import ua.com.lsoft.lincollect.model.main.LoginModel;
import ua.com.lsoft.lincollect.model.main.ResponseResult;
import ua.com.lsoft.lincollect.network.ApiService;
import ua.com.lsoft.lincollect.network.RetrofitApi;
import ua.com.lsoft.lincollect.utils.MethodUtils;
import ua.com.lsoft.lincollect.utils.SharedPrefsUtil;

public class LoginActivity extends AppCompatActivity {

    private EditText usernameEditText;
    private EditText passwordEditText;
    private SurveyDBHelper surveyDBHelper;

    public static final String TOKEN = "token";
    private static final String TAG = LoginActivity.class.getSimpleName();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        getWindow().setSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_HIDDEN);
        surveyDBHelper = SurveyDBHelper.getInstance(this);

        ScrollView scrollView = (ScrollView) findViewById(R.id.scrollView);
        MethodUtils.hideKeyboardOnTouch(scrollView, this);

        usernameEditText = (EditText) findViewById(R.id.username);
        passwordEditText = (EditText) findViewById(R.id.password);

        findViewById(R.id.btnLogin).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (usernameEditText.getText().toString().equals("") || passwordEditText.getText().toString().equals("")) {

                    Toast.makeText(LoginActivity.this, "Login or password could not be empty", Toast.LENGTH_SHORT).show();

                } else {
                    findViewById(R.id.progress_layout).setVisibility(View.VISIBLE);
                    loginRequest(usernameEditText.getText().toString(), passwordEditText.getText().toString());
                }
            }
        });
    }

    private void loginRequest(String login, String password) {
        ApiService apiService = RetrofitApi.getInstance().getApiService();
        Call<ResponseResult> userCall = apiService.loginUser(new LoginModel(login, password));
        userCall.enqueue(new Callback<ResponseResult>() {
            @Override
            public void onResponse(Call<ResponseResult> call, Response<ResponseResult> response) {
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
                if (response.isSuccessful()) {
                    ResponseResult responseResult = response.body();
                    Log.d(TAG, "Authorization " + response.body());

                    if (responseResult.getUser() != null) {
                        SharedPrefsUtil.putStringData(LoginActivity.this, TOKEN, responseResult.getUser().getToken());
                        Intent intent = new Intent(LoginActivity.this, SurveysListActivity.class);
                        startActivity(intent);
                        finish();
                    } else {
                        Toast.makeText(LoginActivity.this, getResources().getString(R.string.wrong_login), Toast.LENGTH_SHORT).show();
                    }
                } else {
                    Log.d(TAG, "Response error " + response.raw().toString());
                    Toast.makeText(LoginActivity.this, getResources().getString(R.string.wrong_login), Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<ResponseResult> call, Throwable t) {
                Log.d(TAG, "Error " + t.getMessage());
                Toast.makeText(LoginActivity.this, t.getMessage(), Toast.LENGTH_LONG).show();
                findViewById(R.id.progress_layout).setVisibility(View.GONE);
            }
        });
    }
}
