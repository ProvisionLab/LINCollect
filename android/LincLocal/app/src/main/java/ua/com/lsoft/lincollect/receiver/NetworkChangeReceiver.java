package ua.com.lsoft.lincollect.receiver;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.util.Log;

import ua.com.lsoft.lincollect.db.SurveyDBHelper;

/**
 * Created by Evgeniy on 26.05.2016.
 */
public class NetworkChangeReceiver extends BroadcastReceiver {

    private static boolean firstConnect = true;

    private SurveyDBHelper surveyDBHelper;

    @Override
    public void onReceive(Context context, Intent intent) {

        surveyDBHelper = SurveyDBHelper.getInstance(context);

        ConnectivityManager cm = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo networkInfo = cm.getActiveNetworkInfo();

        if (networkInfo != null && networkInfo.isConnected()) {
            if (firstConnect) {
                Log.d("JSON", "Connected");
                firstConnect = false;
            }
        } else {
            Log.d("JSON", "Not connected");
            firstConnect = true;
        }
    }
}
