package ua.com.lsoft.lincollect.adapter;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Filter;
import android.widget.TextView;

import java.util.ArrayList;

import ua.com.lsoft.lincollect.R;
import ua.com.lsoft.lincollect.model.main.Company;

/**
 * Created by Evgeniy on 10.04.2017.
 */

public class CompaniesAdapter extends ArrayAdapter<Company> {

    private ArrayList<Company> companyNames;

    public CompaniesAdapter(@NonNull Context context, @NonNull ArrayList<Company> objects) {
        super(context, 0, objects);

        companyNames = new ArrayList<>(objects.size());
        companyNames.addAll(objects);
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {

        Company company = getItem(position);

        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.company_list_item, parent, false);
        }

        TextView companyName = (TextView) convertView.findViewById(R.id.company_name_text);
        companyName.setText(company.getName().replaceAll("\n", ""));

        return convertView;
    }

    @NonNull
    @Override
    public Filter getFilter() {
        return nameFilter;
    }

    Filter nameFilter = new Filter() {
        @Override
        public CharSequence convertResultToString(Object resultValue) {
            return resultValue.toString();
        }

        @Override
        protected FilterResults performFiltering(CharSequence constraint) {

            FilterResults results = new FilterResults();

            if (constraint != null) {
                ArrayList<Company> suggestions = new ArrayList<>();
                for (Company company : companyNames) {
                    if (company.getName().toLowerCase().contains(constraint.toString().toLowerCase())) {
                        suggestions.add(company);
                    }
                }
                results.values = suggestions;
                results.count = suggestions.size();
            }

            return results;
        }

        @Override
        protected void publishResults(CharSequence constraint, FilterResults results) {
            clear();
            if (results != null && results.count > 0) {
                // we have filtered results
                addAll((ArrayList<Company>) results.values);
            }
            notifyDataSetChanged();
        }
    };
}
