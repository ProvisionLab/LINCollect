package ua.com.lsoft.lincollect.fragments;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import java.util.ArrayList;

import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.activity.AddCompanyActivity;
import ua.com.lsoft.lincollect.activity.QuizActivity;
import ua.com.lsoft.lincollect.adapter.CompaniesAdapter;
import ua.com.lsoft.lincollect.db.SurveyDBHelper;
import ua.com.lsoft.lincollect.model.main.Company;
import ua.com.lsoft.lincollect.model.main.RelationshipAnswer;
import ua.com.lsoft.lincollect.utils.MethodUtils;
import ua.com.lsoft.lincollect.utils.SharedPrefsUtil;

import static ua.com.lsoft.lincollect.activity.AddCompanyActivity.ADDED_COMPANIES;
import static ua.com.lsoft.lincollect.activity.AddCompanyActivity.COMPANY_LIST;
import static ua.com.lsoft.lincollect.activity.AddCompanyActivity.RELATION_NODES;
import static ua.com.lsoft.lincollect.fragments.PageFragment.CURRENT;

public class SecondGroupQuestionFragment extends Fragment {

    private ListView selectCompanyList;
    private ListView selectionCompaniesList;
    private EditText searchEditText;
    private ArrayList<Company> selectionList = new ArrayList<>();
    private CompaniesAdapter selectAdapter;
    private CompaniesAdapter selectionAdapter;
    public static final int REQUEST_CODE = 1;
    public static final String COMPANY_NAME = "company";
    public static final String COMPANIES = "companies";
    private static final String RELATIONSHIP_ID = "relationship_id";
    private static final String RELATIONSHIP_POSITION = "rel_position";
    private int currentPosition;
    private long relationshipId;
    private int positionId;
    private ArrayList<Company> companies;
    private ArrayList<Company> companyList;
    private SurveyDBHelper surveyDBHelper;
    private static final String TAG = SecondGroupQuestionFragment.class.getSimpleName();

    public SecondGroupQuestionFragment() {
    }

    public static SecondGroupQuestionFragment newInstance(ArrayList<Company> companies, int position, long id, int posId) {

        Bundle args = new Bundle();

        SecondGroupQuestionFragment fragment = new SecondGroupQuestionFragment();
        args.putSerializable(COMPANIES, companies);
        args.putInt(CURRENT, position);
        args.putLong(RELATIONSHIP_ID, id);
        args.putInt(RELATIONSHIP_POSITION, posId);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        return inflater.inflate(R.layout.company_relationship, container, false);
    }

    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        surveyDBHelper = SurveyDBHelper.getInstance(getActivity());
        currentPosition = getArguments().getInt(CURRENT);
        relationshipId = getArguments().getLong(RELATIONSHIP_ID);
        positionId = getArguments().getInt(RELATIONSHIP_POSITION);
        surveyDBHelper.insertRelationship(new RelationshipAnswer(relationshipId));
        Log.d(TAG, "Position " + currentPosition + " " + relationshipId);
        companies = (ArrayList<Company>) getArguments().getSerializable(COMPANIES);
        companyList = new ArrayList<>(companies);
        searchEditText = (EditText) view.findViewById(R.id.search_company);
        selectCompanyList = (ListView) view.findViewById(R.id.select_list);
        selectAdapter = new CompaniesAdapter(getActivity(), companies);
        selectCompanyList.setAdapter(selectAdapter);

        selectCompanyList.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });

        selectionCompaniesList = (ListView) view.findViewById(R.id.selection_list);
        selectionAdapter = new CompaniesAdapter(getActivity(), selectionList);
        selectionCompaniesList.setAdapter(selectionAdapter);

        selectionCompaniesList.setOnTouchListener(new View.OnTouchListener() {
            // Setting on Touch Listener for handling the touch inside ScrollView
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // Disallow the touch request for parent scroll on touch of child view
                v.getParent().requestDisallowInterceptTouchEvent(true);
                return false;
            }
        });

        selectCompanyList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                selectionList.add(companies.get(position));
                companies.remove(companies.get(position));
                selectAdapter = new CompaniesAdapter(getActivity(), companies);
                selectCompanyList.setAdapter(selectAdapter);
                selectionAdapter.notifyDataSetChanged();
            }
        });

        selectionCompaniesList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                if (!selectionAdapter.getItem(position).isChecked()) {
                    companies.add(selectionList.get(position));
                    selectionAdapter.remove(selectionList.get(position));
                    selectAdapter = new CompaniesAdapter(getActivity(), companies);
                    selectCompanyList.setAdapter(selectAdapter);
                    selectionAdapter.notifyDataSetChanged();
                }
            }
        });

        view.findViewById(R.id.add_company).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getActivity(), AddCompanyActivity.class);

                ArrayList<String> companyNames = new ArrayList<>();
                for (Company company : companyList) {
                    companyNames.add(MethodUtils.formatQuestion(company.getName().toLowerCase()));
                }

                intent.putExtra(COMPANY_LIST, companyNames);
                intent.putExtra(RELATION_NODES, ((QuizActivity) getActivity()).getSurvey().getItems().get(positionId));
                startActivityForResult(intent, REQUEST_CODE);
            }
        });

        searchEditText.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                selectAdapter.getFilter().filter(s);
            }

            @Override
            public void afterTextChanged(Editable s) {
            }
        });

        view.findViewById(R.id.nextBtn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (selectionAdapter.getCount() > 0) {

                    for (Company company : selectionList) {
                        company.setRelationshipId(relationshipId);
                        surveyDBHelper.insertCompany(company);
                    }
                    Log.d(TAG, "Companies " + selectionList);

                    FragmentTransaction transaction = getActivity().getFragmentManager().beginTransaction();
                    transaction.setCustomAnimations(R.animator.start_left_transition, R.animator.end_left_transition);
                    transaction.replace(R.id.fragment_layout,
                            ((QuizActivity) getActivity()).getFragments().get(currentPosition + 1)).commit();
                    SharedPrefsUtil.clearValue(getActivity(), ADDED_COMPANIES);
                } else {
                    Toast.makeText(getActivity(), getString(R.string.add_companies), Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode == Activity.RESULT_OK && requestCode == REQUEST_CODE) {
            Company company = (Company) data.getSerializableExtra(COMPANY_NAME);
            selectionList.add(company);
            selectionAdapter.notifyDataSetChanged();
        }
    }
}