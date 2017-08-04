package ua.com.lsoft.lincollect.utils;

import android.app.Activity;
import android.view.MotionEvent;
import android.view.View;
import android.view.inputmethod.InputMethodManager;

/**
 * Created by Evgeniy on 17.05.2016.
 */
public class MethodUtils {

    /**
     * Add zero before number
     *
     * @param number
     * @param type   0 - day, 1 - month
     * @return
     */
    public static String addZeroToDayMonth(int number, int type) {
        switch (type) {
            case 0:
                return number < 10 ? "0" + number : String.valueOf(number);
            case 1:
                return (number + 1) < 10 ? "0" + (number + 1) : String.valueOf(number + 1);
            default:
                return "";
        }
    }

    /**
     * Hide soft input keyboard on view touch event
     *
     * @param view
     * @param activity
     */
    public static void hideKeyboardOnTouch(View view, final Activity activity) {
        view.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                InputMethodManager inputMethodManager = (InputMethodManager) activity.getSystemService(Activity.INPUT_METHOD_SERVICE);
                if (activity.getCurrentFocus() != null) {
                    inputMethodManager.hideSoftInputFromWindow(activity.getCurrentFocus().getWindowToken(), 0);
                }
                return false;
            }
        });
    }

    public static String formatQuestion(String text) {
        return text
                .replaceAll("<.*?>", "")
                .replaceAll("\r", "")
                .replaceAll("\n", "");
    }
}
