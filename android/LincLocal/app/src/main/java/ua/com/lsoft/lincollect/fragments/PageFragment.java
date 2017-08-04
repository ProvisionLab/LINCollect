package ua.com.lsoft.lincollect.fragments;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.text.InputType;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.SeekBar;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;

import java.util.ArrayList;

import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.activity.QuizActivity;
import ua.com.lsoft.lincollect.activity.SurveyEndActivity;
import ua.com.lsoft.lincollect.adapter.DefaultValuesAdapter;
import ua.com.lsoft.lincollect.db.SurveyDBHelper;
import ua.com.lsoft.lincollect.model.additional.Matrix;
import ua.com.lsoft.lincollect.model.additional.ViewModel;
import ua.com.lsoft.lincollect.model.main.Answer;
import ua.com.lsoft.lincollect.model.main.Question;
import ua.com.lsoft.lincollect.model.main.QuestionAnswer;
import ua.com.lsoft.lincollect.model.main.Relationship;
import ua.com.lsoft.lincollect.model.main.RelationshipNodeQuestionAnswer;
import ua.com.lsoft.lincollect.model.main.RelationshipQuestionAnswer;
import ua.com.lsoft.lincollect.model.main.Section;
import ua.com.lsoft.lincollect.model.main.SectionAnswers;
import ua.com.lsoft.lincollect.utils.MethodUtils;
import ua.com.lsoft.lincollect.utils.SharedPrefsUtil;
import ua.com.lsoft.lincollect.widget.CustomTableLayout;

import static ua.com.lsoft.lincollect.activity.StartActivity.SURVEY_ID;
import static ua.com.lsoft.lincollect.activity.StartActivity.SURVEY_NAME;

public class PageFragment extends Fragment {

    private SurveyDBHelper surveyDBHelper;
    private LinearLayout pageLayout;
    private ArrayList<Question> questions;
    private Section section;
    private Relationship relationship;
    private ArrayList<ViewModel> viewModels;
    private int currentPosition;
    private Button nextButton;

    private static final String SECTION = "section";
    private static final String RELATIONSHIP = "relationship";
    public static final String CURRENT = "current";
    public static final String FRAGMENT_TAG = "fragment";
    public static final String JSON_BEFORE = "json_before";
    public static final String JSON_AFTER = "json_after";
    private static final String TAG = PageFragment.class.getSimpleName();

    public PageFragment() {
    }

    public static PageFragment newInstance(Section section, int position, String tag) {
        PageFragment pageFragment = new PageFragment();
        Bundle args = new Bundle();
        args.putSerializable(SECTION, section);
        args.putInt(CURRENT, position);
        args.putString(FRAGMENT_TAG, tag);
        pageFragment.setArguments(args);

        return pageFragment;
    }

    public static PageFragment newInstance(Relationship relationship, int position) {
        PageFragment pageFragment = new PageFragment();
        Bundle args = new Bundle();
        args.putSerializable(RELATIONSHIP, relationship);
        args.putInt(CURRENT, position);
        pageFragment.setArguments(args);

        return pageFragment;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_page, container, false);
    }

    @Override
    public void onViewCreated(final View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        pageLayout = (LinearLayout) view.findViewById(R.id.page_layout);
        MethodUtils.hideKeyboardOnTouch(view, getActivity());

        section = (Section) getArguments().getSerializable(SECTION);
        relationship = (Relationship) getArguments().getSerializable(RELATIONSHIP);
        currentPosition = getArguments().getInt(CURRENT);
        Log.d(TAG, "Position " + currentPosition);

        nextButton = (Button) view.findViewById(R.id.nextBtn);

        surveyDBHelper = SurveyDBHelper.getInstance(getActivity());

        if (currentPosition == -1) {
            questions = relationship.getNodeQuestions();
            nextButton.setVisibility(View.GONE);
        } else if (section != null) {
            questions = section.getQuestions();
        } else if (relationship != null) {
            questions = relationship.getQuestions();
        }

        viewModels = new ArrayList<>();

        if (questions != null) {
            for (final Question question : questions) {

                switch (question.getQuestionFormat().getName()) {
                    case "Text":
                        LinearLayout textQuestionLayout = (LinearLayout) LayoutInflater.from(getActivity())
                                .inflate(R.layout.question_edittext, null);
                        TextView textQuestionLabel = (TextView) textQuestionLayout.findViewById(R.id.label);
                        textQuestionLabel.setText(question.getOrderId() + ". " + MethodUtils.formatQuestion(question.getText()));

                        final EditText textQuestionEditText = (EditText) textQuestionLayout.findViewById(R.id.edit);
                        textQuestionEditText.setTag(question.getId());

                        if (question.getTextRowsCount() > 1) {
                            textQuestionEditText.setMinLines(question.getTextRowsCount());
                            textQuestionEditText.setMaxLines(question.getTextRowsCount());
                            textQuestionEditText.setGravity(Gravity.TOP);
                            textQuestionEditText.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_FLAG_MULTI_LINE);
                        }

                        viewModels.add(new ViewModel(
                                question.getId(),
                                textQuestionLabel,
                                new ArrayList<View>() {{
                                    add(textQuestionEditText);
                                }},
                                "Text",
                                question.isCompulsory()
                        ));

                        pageLayout.addView(textQuestionLayout);
                        break;
                    case "Choice Across":
                        TextView choiceAcrossQuestionTextView = (TextView) LayoutInflater.from(getActivity()).inflate(R.layout.question_textview, pageLayout, false);
                        choiceAcrossQuestionTextView.setText(question.getOrderId() + ". " + MethodUtils.formatQuestion(question.getText()));
                        pageLayout.addView(choiceAcrossQuestionTextView);

                        if (question.isMultiple()) {
                            LinearLayout checkboxLayout = new LinearLayout(getActivity());
                            checkboxLayout.setOrientation(LinearLayout.HORIZONTAL);

                            ArrayList<View> views = new ArrayList<>();

                            for (Answer answer : question.getAnswers()) {
                                CheckBox checkBox = (CheckBox) LayoutInflater.from(getActivity()).inflate(R.layout.checkbox_layout, pageLayout, false);
                                checkBox.setTag(answer.getValue());
                                checkBox.setText(answer.getText());

                                if (answer.isDefault()) {
                                    checkBox.setChecked(true);
                                }

                                views.add(checkBox);
                                checkboxLayout.addView(checkBox);
                            }

                            viewModels.add(new ViewModel(
                                    question.getId(),
                                    choiceAcrossQuestionTextView,
                                    views,
                                    "Checkbox Choice Across",
                                    question.isCompulsory()
                            ));

                            pageLayout.addView(checkboxLayout);
                        } else {
                            ArrayList<View> views = new ArrayList<>();

                            RadioGroup radioGroup = new RadioGroup(getActivity());
                            for (Answer answer : question.getAnswers()) {
                                RadioButton radioButton = (RadioButton) LayoutInflater.from(getActivity()).inflate(R.layout.radiobutton_layout, pageLayout, false);
                                radioButton.setTag(answer.getValue());
                                radioButton.setText(answer.getText());

                                if (answer.isDefault()) {
                                    radioButton.setChecked(true);
                                }

                                views.add(radioButton);
                                radioGroup.setOrientation(LinearLayout.HORIZONTAL);
                                radioGroup.addView(radioButton);
                            }

                            viewModels.add(new ViewModel(
                                    question.getId(),
                                    choiceAcrossQuestionTextView,
                                    views,
                                    "RadioButton Choice Across",
                                    question.isCompulsory()
                            ));

                            pageLayout.addView(radioGroup);
                        }
                        break;
                    case "Choice Down":
                        TextView choiceDownQuestionTextView = (TextView) LayoutInflater.from(getActivity()).inflate(R.layout.question_textview, pageLayout, false);
                        choiceDownQuestionTextView.setText(question.getOrderId() + ". " + MethodUtils.formatQuestion(question.getText()));
                        pageLayout.addView(choiceDownQuestionTextView);

                        if (question.isMultiple()) {
                            LinearLayout checkboxLayout = new LinearLayout(getActivity());
                            checkboxLayout.setOrientation(LinearLayout.VERTICAL);

                            ArrayList<View> views = new ArrayList<>();

                            for (Answer answer : question.getAnswers()) {
                                CheckBox checkBox = (CheckBox) LayoutInflater.from(getActivity()).inflate(R.layout.checkbox_layout, pageLayout, false);
                                checkBox.setTag(answer.getValue());
                                checkBox.setText(answer.getText());

                                if (answer.isDefault()) {
                                    checkBox.setChecked(true);
                                }

                                views.add(checkBox);
                                checkboxLayout.addView(checkBox);
                            }

                            viewModels.add(new ViewModel(
                                    question.getId(),
                                    choiceDownQuestionTextView,
                                    views,
                                    "Checkbox Choice Down",
                                    question.isCompulsory()
                            ));

                            pageLayout.addView(checkboxLayout);
                        } else {
                            ArrayList<View> views = new ArrayList<>();

                            RadioGroup radioGroup = new RadioGroup(getActivity());
                            for (Answer answer : question.getAnswers()) {
                                RadioButton radioButton = (RadioButton) LayoutInflater.from(getActivity()).inflate(R.layout.radiobutton_layout, pageLayout, false);
                                radioButton.setTag(answer.getValue());
                                radioButton.setText(answer.getText());

                                if (answer.isDefault()) {
                                    radioButton.setChecked(true);
                                }

                                views.add(radioButton);
                                radioGroup.setOrientation(LinearLayout.VERTICAL);
                                radioGroup.addView(radioButton);
                            }

                            viewModels.add(new ViewModel(
                                    question.getId(),
                                    choiceDownQuestionTextView,
                                    views,
                                    "RadioButton Choice Down",
                                    question.isCompulsory()
                            ));

                            pageLayout.addView(radioGroup);
                        }
                        break;
                    case "Drop Down":
                        TextView dropdownQuestionTextView = (TextView) LayoutInflater.from(getActivity()).inflate(R.layout.question_textview, pageLayout, false);
                        dropdownQuestionTextView.setText(question.getOrderId() + ". " + MethodUtils.formatQuestion(question.getText()));
                        pageLayout.addView(dropdownQuestionTextView);

                        final Spinner spinner = new Spinner(getActivity());
                        spinner.setTag(question.getId());
                        DefaultValuesAdapter defaultValuesAdapter = new DefaultValuesAdapter(getActivity(),
                                android.R.layout.simple_dropdown_item_1line, question.getAnswers());
                        spinner.setAdapter(defaultValuesAdapter);
                        defaultValuesAdapter.setDropDownViewResource(R.layout.dropdown_item);

                        viewModels.add(new ViewModel(
                                question.getId(),
                                dropdownQuestionTextView,
                                new ArrayList<View>() {{
                                    add(spinner);
                                }},
                                "DropdownList",
                                question.isCompulsory()
                        ));
                        pageLayout.addView(spinner);
                        break;
                    case "Matrix":
                        TextView matrixQuestionTextView = (TextView) LayoutInflater.from(getActivity()).inflate(R.layout.question_textview, pageLayout, false);
                        matrixQuestionTextView.setText(question.getOrderId() + ". " + MethodUtils.formatQuestion(question.getText()));
                        pageLayout.addView(matrixQuestionTextView);

                        String[] rows = question.getRows().split(",");
                        ArrayList<Answer> answers = question.getAnswers();

                        Matrix matrix = new Matrix(question.getId(), answers, rows);
                        Log.d("Matrix", "Matrix: " + matrix);
                        CustomTableLayout customTableLayout = new CustomTableLayout(getActivity(), matrix);

                        viewModels.add(new ViewModel(
                                question.getId(),
                                matrixQuestionTextView,
                                customTableLayout.getViews(),
                                "Matrix",
                                question.isCompulsory()
                        ));

                        TableLayout tableLayout = customTableLayout.init();
                        pageLayout.addView(tableLayout);
                        break;
                    case "Slider":
                        TextView sliderQuestionTextView = (TextView) LayoutInflater.from(getActivity()).inflate(R.layout.question_textview, pageLayout, false);
                        sliderQuestionTextView.setText(question.getOrderId() + ". " + MethodUtils.formatQuestion(question.getText()));
                        pageLayout.addView(sliderQuestionTextView);

                        LinearLayout linearLayout = (LinearLayout) LayoutInflater.from(getActivity()).inflate(R.layout.seekbar_layout, pageLayout, false);
                        LinearLayout labelLayout = (LinearLayout) linearLayout.findViewById(R.id.label_layout);
                        TextView startLabel = (TextView) labelLayout.findViewById(R.id.start_label);
                        startLabel.setText(question.getTextMin());
                        final TextView progressLabel = (TextView) labelLayout.findViewById(R.id.progress);
                        TextView endLabel = (TextView) labelLayout.findViewById(R.id.end_label);
                        endLabel.setText(question.getTextMax());

                        int minValue = Integer.parseInt(question.getValueMin());
                        int maxValue = Integer.parseInt(question.getValueMax());

                        final SeekBar seekBar = (SeekBar) linearLayout.findViewById(R.id.seekbar);
                        seekBar.setTag(question.getId());
                        seekBar.setMax(maxValue);
                        seekBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
                            @Override
                            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                                progressLabel.setText(String.valueOf(progress));
                            }

                            @Override
                            public void onStartTrackingTouch(SeekBar seekBar) {
                            }

                            @Override
                            public void onStopTrackingTouch(SeekBar seekBar) {
                            }
                        });

                        viewModels.add(new ViewModel(
                                question.getId(),
                                sliderQuestionTextView,
                                new ArrayList<View>() {{
                                    add(seekBar);
                                }},
                                "Slider",
                                question.isCompulsory()
                        ));

                        pageLayout.addView(linearLayout);
                        break;
                    default:
                        Log.e("Error", "There is no such type!");
                }
            }
        }

        nextButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                ArrayList<Boolean> isAllValidList = validation(viewModels);

                if (!isAllValidList.contains(false)) {

                    if (section != null) {

                        SectionAnswers sectionAnswers = getAnswersFromModels(section.getId(), viewModels);
                        Log.d(TAG, "SectionAnswer " + sectionAnswers);

                        if (getArguments().getString(FRAGMENT_TAG).equals("Before")) {
                            String jsonStringBefore = new Gson().toJson(sectionAnswers);
                            SharedPrefsUtil.putStringData(getActivity(), JSON_BEFORE, jsonStringBefore);
                        } else if (getArguments().getString(FRAGMENT_TAG).equals("After")) {
                            String jsonStringAfter = new Gson().toJson(sectionAnswers);
                            SharedPrefsUtil.putStringData(getActivity(), JSON_AFTER, jsonStringAfter);
                        }
                    } else if (relationship != null) {
                        getRelationshipAnswersFromModels(relationship.getId(), viewModels, "relationship", null);
                    }

                    if (currentPosition != ((QuizActivity) getActivity()).getFragments().size() - 1) {

                        FragmentTransaction transaction = getActivity().getFragmentManager().beginTransaction();
                        transaction.setCustomAnimations(R.animator.start_left_transition, R.animator.end_left_transition);
                        transaction.replace(R.id.fragment_layout,
                                ((QuizActivity) getActivity()).getFragments().get(currentPosition + 1)).commit();
                    } else {
                        Intent intent = new Intent(getActivity(), SurveyEndActivity.class);
                        intent.putExtra(SURVEY_ID, ((QuizActivity) getActivity()).getSurvey().getSurveyId());
                        intent.putExtra(SURVEY_NAME, ((QuizActivity) getActivity()).getSurvey().getSurveyName());
                        startActivity(intent);
                        getActivity().finish();
                    }
                } else {
                    Toast.makeText(getActivity(), getString(R.string.answer_mandatory), Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
    }

    private boolean isViewChecked(ArrayList<View> views) {

        if (views.get(0) instanceof CheckBox) {
            for (View view : views) {
                CheckBox checkBox = (CheckBox) view;
                if (checkBox.isChecked()) {
                    return true;
                }
            }
        } else if (views.get(0) instanceof RadioButton) {
            for (View view : views) {
                RadioButton radioButton = (RadioButton) view;
                if (radioButton.isChecked()) {
                    return true;
                }
            }
        }

        return false;
    }

    private boolean isEditTextEmpty(ArrayList<View> views) {
        for (View view : views) {
            EditText editText = (EditText) view;
            if (editText.getText().toString().isEmpty()) {
                return true;
            }
        }

        return false;
    }

    /**
     * Add * and red color for TextView
     *
     * @param viewModel
     * @param questionText
     */
    private void setMandatoryWarning(ViewModel viewModel, String questionText) {
        viewModel.getQuestionText().setTextColor(Color.RED);
        if (questionText.charAt(questionText.length() - 1) != '*') {
            viewModel.getQuestionText().setText(String.format("%s*", questionText));
        }
    }

    /**
     * Remove * and red color for TextView
     *
     * @param viewModel
     * @param questionText
     */
    private void removeMandatoryWarning(ViewModel viewModel, String questionText) {
        viewModel.getQuestionText().setTextColor(Color.BLACK);
        int index = questionText.lastIndexOf("*");
        if (index >= 0) {
            questionText = new StringBuilder(questionText).replace(index, index + 1, "").toString();
        }
        viewModel.getQuestionText().setText(questionText);
    }

    public ArrayList<Boolean> validation(ArrayList<ViewModel> viewModels) {
        ArrayList<Boolean> bools = new ArrayList<>();

        Log.d("Moving", "Models " + viewModels);
        for (ViewModel viewModel : viewModels) {
            ArrayList<View> views = viewModel.getViews();
            if (viewModel.isMandatory()) {
                String questionText = viewModel.getQuestionText().getText().toString();
                switch (viewModel.getFieldType()) {
                    case "Text":
                        TextView textView = (TextView) viewModel.getViews().get(0);
                        if (textView.getText().toString().trim().isEmpty()) {
                            setMandatoryWarning(viewModel, questionText);
                            bools.add(false);
                        } else {
                            removeMandatoryWarning(viewModel, questionText);
                            bools.add(true);
                        }
                        break;
                    case "RadioButton Choice Across":
                    case "Checkbox Choice Across":
                    case "RadioButton Choice Down":
                    case "Checkbox Choice Down":
                    case "Matrix":
                        if (!isViewChecked(views)) {
                            setMandatoryWarning(viewModel, questionText);
                            bools.add(false);
                        } else {
                            removeMandatoryWarning(viewModel, questionText);
                            bools.add(true);
                        }
                        break;
                }
            }
        }

        return bools;
    }

    private SectionAnswers getAnswersFromModels(final long sectionId, final ArrayList<ViewModel> viewModels) {

        ArrayList<QuestionAnswer> questionAnswers = new ArrayList<>();

        for (ViewModel viewModel : viewModels) {
            switch (viewModel.getFieldType()) {
                case "Text":
                    final EditText editText = (EditText) viewModel.getViews().get(0);

                    ArrayList<String> textValues = new ArrayList<>();
                    String textValue = editText.getText().toString();
                    textValues.add(textValue);

                    QuestionAnswer textQuestionAnswer = new QuestionAnswer(viewModel.getQuestionId(), textValues);
                    questionAnswers.add(textQuestionAnswer);
                    break;
                case "DropdownList":
                    final Spinner spinner = (Spinner) viewModel.getViews().get(0);

                    ArrayList<String> dropdownValues = new ArrayList<>();
                    String dropDownValue = ((Answer) spinner.getSelectedItem()).getValue();
                    dropdownValues.add(dropDownValue);

                    QuestionAnswer dropdownQuestionAnswer = new QuestionAnswer(viewModel.getQuestionId(), dropdownValues);
                    questionAnswers.add(dropdownQuestionAnswer);
                    break;
                case "Slider":
                    final SeekBar seekBar = (SeekBar) viewModel.getViews().get(0);

                    ArrayList<String> sliderValues = new ArrayList<>();
                    String sliderValue = String.valueOf(seekBar.getProgress());
                    sliderValues.add(sliderValue);

                    QuestionAnswer sliderQuestionAnswer = new QuestionAnswer(viewModel.getQuestionId(), sliderValues);
                    questionAnswers.add(sliderQuestionAnswer);
                    break;
                case "RadioButton Choice Across":
                case "RadioButton Choice Down":
                case "Matrix":
                    ArrayList<String> radioValues = new ArrayList<>();

                    for (View view : viewModel.getViews()) {
                        RadioButton radioButton = (RadioButton) view;
                        if (radioButton.isChecked()) {
                            radioValues.add(radioButton.getTag().toString());
                        }
                    }

                    QuestionAnswer radioQuestionAnswer = new QuestionAnswer(viewModel.getQuestionId(), radioValues);
                    questionAnswers.add(radioQuestionAnswer);
                    break;
                case "Checkbox Choice Across":
                case "Checkbox Choice Down":
                    ArrayList<String> checkValues = new ArrayList<>();

                    for (View view : viewModel.getViews()) {
                        CheckBox checkBox = (CheckBox) view;
                        if (checkBox.isChecked()) {
                            checkValues.add(checkBox.getTag().toString());
                        }
                    }

                    QuestionAnswer checkQuestionAnswer = new QuestionAnswer(viewModel.getQuestionId(), checkValues);
                    questionAnswers.add(checkQuestionAnswer);
                    break;
            }
        }

        return new SectionAnswers(sectionId, questionAnswers);
    }

    public void getRelationshipAnswersFromModels(final long relationshipId, final ArrayList<ViewModel> viewModels, String tag, String companyName) {

        for (ViewModel viewModel : viewModels) {
            switch (viewModel.getFieldType()) {
                case "Text":
                    final EditText editText = (EditText) viewModel.getViews().get(0);

                    ArrayList<String> textValues = new ArrayList<>();
                    String textValue = editText.getText().toString();
                    textValues.add(textValue);

                    if (tag.equals("relationship")) {
                        RelationshipQuestionAnswer textQuestionAnswer = new RelationshipQuestionAnswer(viewModel.getQuestionId(), relationshipId, textValues);
                        surveyDBHelper.insertRelationshipQuestionAnswer(textQuestionAnswer);
                    } else {
                        RelationshipNodeQuestionAnswer textQuestionAnswer = new RelationshipNodeQuestionAnswer(viewModel.getQuestionId(), relationshipId, textValues, companyName);
                        surveyDBHelper.insertRelationshipNodeQuestionAnswer(textQuestionAnswer);
                    }
                    break;
                case "DropdownList":
                    final Spinner spinner = (Spinner) viewModel.getViews().get(0);

                    ArrayList<String> dropdownValues = new ArrayList<>();
                    String dropDownValue = ((Answer) spinner.getSelectedItem()).getValue();
                    dropdownValues.add(dropDownValue);

                    if (tag.equals("relationship")) {
                        RelationshipQuestionAnswer dropdownQuestionAnswer = new RelationshipQuestionAnswer(viewModel.getQuestionId(), relationshipId, dropdownValues);
                        surveyDBHelper.insertRelationshipQuestionAnswer(dropdownQuestionAnswer);
                    } else {
                        RelationshipNodeQuestionAnswer textQuestionAnswer = new RelationshipNodeQuestionAnswer(viewModel.getQuestionId(), relationshipId, dropdownValues, companyName);
                        surveyDBHelper.insertRelationshipNodeQuestionAnswer(textQuestionAnswer);
                    }
                    break;
                case "Slider":
                    final SeekBar seekBar = (SeekBar) viewModel.getViews().get(0);

                    ArrayList<String> sliderValues = new ArrayList<>();
                    String sliderValue = String.valueOf(seekBar.getProgress());
                    sliderValues.add(sliderValue);

                    if (tag.equals("relationship")) {
                        RelationshipQuestionAnswer sliderQuestionAnswer = new RelationshipQuestionAnswer(viewModel.getQuestionId(), relationshipId, sliderValues);
                        surveyDBHelper.insertRelationshipQuestionAnswer(sliderQuestionAnswer);
                    } else {
                        RelationshipNodeQuestionAnswer textQuestionAnswer = new RelationshipNodeQuestionAnswer(viewModel.getQuestionId(), relationshipId, sliderValues, companyName);
                        surveyDBHelper.insertRelationshipNodeQuestionAnswer(textQuestionAnswer);
                    }
                    break;
                case "RadioButton Choice Across":
                case "RadioButton Choice Down":
                case "Matrix":
                    ArrayList<String> radioValues = new ArrayList<>();

                    for (View view : viewModel.getViews()) {
                        RadioButton radioButton = (RadioButton) view;
                        if (radioButton.isChecked()) {
                            radioValues.add(radioButton.getTag().toString());
                        }
                    }

                    if (tag.equals("relationship")) {
                        RelationshipQuestionAnswer radioQuestionAnswer = new RelationshipQuestionAnswer(viewModel.getQuestionId(), relationshipId, radioValues);
                        surveyDBHelper.insertRelationshipQuestionAnswer(radioQuestionAnswer);
                    } else {
                        RelationshipNodeQuestionAnswer textQuestionAnswer = new RelationshipNodeQuestionAnswer(viewModel.getQuestionId(), relationshipId, radioValues, companyName);
                        surveyDBHelper.insertRelationshipNodeQuestionAnswer(textQuestionAnswer);
                    }
                    break;
                case "Checkbox Choice Across":
                case "Checkbox Choice Down":
                    ArrayList<String> checkValues = new ArrayList<>();

                    for (View view : viewModel.getViews()) {
                        CheckBox checkBox = (CheckBox) view;
                        if (checkBox.isChecked()) {
                            checkValues.add(checkBox.getTag().toString());
                        }
                    }

                    if (tag.equals("relationship")) {
                        RelationshipQuestionAnswer checkQuestionAnswer = new RelationshipQuestionAnswer(viewModel.getQuestionId(), relationshipId, checkValues);
                        surveyDBHelper.insertRelationshipQuestionAnswer(checkQuestionAnswer);
                    } else {
                        RelationshipNodeQuestionAnswer textQuestionAnswer = new RelationshipNodeQuestionAnswer(viewModel.getQuestionId(), relationshipId, checkValues, companyName);
                        surveyDBHelper.insertRelationshipNodeQuestionAnswer(textQuestionAnswer);
                    }
                    break;
            }
        }
    }

    public ArrayList<ViewModel> getViewModels() {
        return viewModels;
    }

    public void setViewModels(ArrayList<ViewModel> viewModels) {
        this.viewModels = viewModels;
    }
}
