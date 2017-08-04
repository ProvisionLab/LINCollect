package ua.com.lsoft.lincollect.widget;

import android.content.Context;
import android.support.v7.widget.AppCompatRadioButton;
import android.view.Gravity;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.RadioGroup;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import java.util.ArrayList;

import ua.com.lsoft.lincollect.model.additional.Matrix;

/**
 * Created by Evgeniy on 22.04.2016.
 */
public class CustomTableLayout extends TableLayout {

    private Context context;
    private Matrix matrix;
    private ArrayList<View> views;

    public CustomTableLayout(Context context, Matrix matrix) {
        super(context);
        this.context = context;
        this.matrix = matrix;
        views = new ArrayList<>();
    }

    public ArrayList<View> getViews() {
        return views;
    }

    public void setViews(ArrayList<View> views) {
        this.views = views;
    }

    public TableLayout init() {

        TableLayout.LayoutParams tableParams =
                new TableLayout.LayoutParams(TableLayout.LayoutParams.MATCH_PARENT,
                        LayoutParams.WRAP_CONTENT);
        TableLayout.LayoutParams rowParams =
                new TableLayout.LayoutParams(TableLayout.LayoutParams.MATCH_PARENT, TableLayout.LayoutParams.WRAP_CONTENT);
        TableRow.LayoutParams itemParams =
                new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT, TableRow.LayoutParams.WRAP_CONTENT, 1f);
        TableRow.LayoutParams firstItemParams = new TableRow.LayoutParams(200, TableRow.LayoutParams.WRAP_CONTENT);
        TableRow.LayoutParams firstItemsParams = new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT, TableRow.LayoutParams.WRAP_CONTENT, 1f);
        RadioGroup.LayoutParams radioGroupParams = new RadioGroup.LayoutParams(RadioGroup.LayoutParams.MATCH_PARENT, RadioGroup.LayoutParams.WRAP_CONTENT, 1f);

        TableLayout tableLayout = new TableLayout(context);
        tableLayout.setLayoutParams(tableParams);

        LinearLayout headerLayout = new LinearLayout(context);
        headerLayout.setOrientation(HORIZONTAL);
        TextView emptyTextView = new TextView(context);
        emptyTextView.setText("");
        emptyTextView.setLayoutParams(firstItemParams);
        headerLayout.addView(emptyTextView);
        for (int j = 0; j < matrix.getColumns().size(); j++) {
            TextView columnsHeaderTextView = new TextView(context);
            columnsHeaderTextView.setText(matrix.getColumns().get(j).getText());
            columnsHeaderTextView.setLayoutParams(firstItemsParams);
            columnsHeaderTextView.setPadding(5, 5, 5, 5);
            columnsHeaderTextView.setGravity(Gravity.CENTER_VERTICAL);
            headerLayout.addView(columnsHeaderTextView);
        }
        headerLayout.setLayoutParams(rowParams);
        tableLayout.addView(headerLayout);

        for (int i = 0; i < matrix.getRows().length; i++) {
            TableRow tableRow = new TableRow(context);

            TextView rowHeaderTextView = new TextView(context);
            rowHeaderTextView.setLayoutParams(firstItemParams);
            rowHeaderTextView.setText(matrix.getRows()[i]);
            rowHeaderTextView.setPadding(5, 5, 5, 5);
            rowHeaderTextView.setGravity(Gravity.CENTER);
            tableRow.addView(rowHeaderTextView);

            RadioGroup radioGroup = new RadioGroup(context);
            radioGroup.setOrientation(HORIZONTAL);
            for (int j = 0; j < matrix.getColumns().size(); j++) {
                AppCompatRadioButton radioButton = new AppCompatRadioButton(context);
                radioButton.setLayoutParams(radioGroupParams);
                radioButton.setTag(matrix.getColumns().get(j).getValue());
                views.add(radioButton);
                radioGroup.addView(radioButton);
            }
            radioGroup.setLayoutParams(itemParams);
            tableRow.addView(radioGroup);
            tableRow.setLayoutParams(rowParams);
            tableLayout.addView(tableRow);

            tableLayout.setTag("RadioButton");
        }

        return tableLayout;
    }
}
