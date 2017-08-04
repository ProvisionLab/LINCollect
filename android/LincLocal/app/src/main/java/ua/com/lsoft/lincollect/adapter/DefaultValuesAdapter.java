package ua.com.lsoft.lincollect.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.util.ArrayList;

import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.model.main.Answer;

/**
 * Created by Evgeniy on 10.04.2017.
 */

public class DefaultValuesAdapter extends ArrayAdapter<Answer> {

    public DefaultValuesAdapter(Context context, int resource, ArrayList<Answer> objects) {
        super(context, resource, objects);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        Answer defaultValue = getItem(position);

        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(android.R.layout.simple_dropdown_item_1line, parent, false);
        }

        TextView defValueTextView = (TextView) convertView.findViewById(android.R.id.text1);
        defValueTextView.setText(defaultValue.getText());

        return convertView;
    }

    @Override
    public View getDropDownView(int position, View convertView, ViewGroup parent) {
        Answer defaultValue = getItem(position);

        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.dropdown_item, parent, false);
        }

        TextView defValueTextView = (TextView) convertView.findViewById(R.id.list_item_text);
        defValueTextView.setText(defaultValue.getText());

        return convertView;
    }
}
