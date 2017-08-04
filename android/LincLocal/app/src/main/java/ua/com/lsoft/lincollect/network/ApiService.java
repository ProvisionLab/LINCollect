package ua.com.lsoft.lincollect.network;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.Headers;
import retrofit2.http.POST;
import retrofit2.http.Path;
import ua.com.lsoft.lincollect.model.main.AnswersModel;
import ua.com.lsoft.lincollect.model.main.LoginModel;
import ua.com.lsoft.lincollect.model.main.PassSurvey;
import ua.com.lsoft.lincollect.model.main.ResponseData;
import ua.com.lsoft.lincollect.model.main.ResponseResult;
import ua.com.lsoft.lincollect.model.main.StartSurvey;
import ua.com.lsoft.lincollect.model.main.StartSurveyId;
import ua.com.lsoft.lincollect.model.main.Survey;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public interface ApiService {

    @Headers("Content-Type: application/json")
    @POST("api/login")
    Call<ResponseResult> loginUser(@Body LoginModel loginModel);

    @Headers("Content-Type: application/json")
    @GET("api/survey")
    Call<ResponseData<ArrayList<Survey>>> getSurveys(@Header("Authorization") String token);

    @Headers("Content-Type: application/json")
    @POST("api/survey/start")
    Call<ResponseData<StartSurveyId>> getSurveyId(@Header("Authorization") String token, @Body StartSurvey startSurvey);

    @Headers("Content-Type: application/json")
    @GET("api/survey/pass/{id}")
    Call<ResponseData<PassSurvey>> getSurveyById(@Header("Authorization") String token, @Path("id") long id);

    @Headers("Content-Type: application/json")
    @POST("api/survey/submit")
    Call<ResponseData> sendSurveyAnswers(@Header("Authorization") String token, @Body AnswersModel answersModel);
}
