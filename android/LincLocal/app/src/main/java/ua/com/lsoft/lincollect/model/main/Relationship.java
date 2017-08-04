package ua.com.lsoft.lincollect.model.main;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Evgeniy on 04.04.2017.
 */

public class Relationship implements Serializable {

    @SerializedName("Id")
    private long id;

    @SerializedName("SurveyId")
    private long surveyId;

    @SerializedName("OrderId")
    private long orderId;

    @SerializedName("Name")
    private String name;

    @SerializedName("NodeList")
    private String nodeList;

    @SerializedName("QuestionLayoutId")
    private long questionLayoutId;

    @SerializedName("QuestionLayout")
    private QuestionFormat questionLayout;

    @SerializedName("MaximumNodes")
    private int maxNodes;

    @SerializedName("SortNodeList")
    private boolean sortNodes;

    @SerializedName("AddNodes")
    private boolean addNodes;

    @SerializedName("HideAddedNodes")
    private boolean hideAddedNodes;

    @SerializedName("AllowSelectAllNodes")
    private boolean allowSelectAllNodes;

    @SerializedName("CanSkip")
    private boolean canSkip;

    @SerializedName("UseDDSearch")
    private boolean useDDSearch;

    @SerializedName("SuperUserViewNodes")
    private boolean superUserViewNodes;

    @SerializedName("NodeSelectionId")
    private int nodeSelectionId;

    @SerializedName("NodeSelection")
    private QuestionFormat nodeSelection;

    @SerializedName("GeneratorName")
    private String generatorName;

    @SerializedName("Companies")
    private Companies companies;

    @SerializedName("Questions")
    private ArrayList<Question> questions;

    @SerializedName("NodeQuestions")
    private ArrayList<Question> nodeQuestions;

    @SerializedName("QuestionAnswers")
    private ArrayList<QuestionAnswer> questionAnswers;

    @SerializedName("NQuestionAnswers")
    private ArrayList<QuestionAnswer> nQuestionAnswers;

    public Relationship() {
    }

    public Relationship(long id, long surveyId, long orderId, String name, String nodeList, long questionLayoutId,
                        QuestionFormat questionLayout, int maxNodes, boolean sortNodes, boolean addNodes,
                        boolean hideAddedNodes, boolean allowSelectAllNodes, boolean canSkip, boolean useDDSearch,
                        boolean superUserViewNodes, int nodeSelectionId, QuestionFormat nodeSelection,
                        String generatorName, Companies companies, ArrayList<Question> questions, ArrayList<Question> nodeQuestions,
                        ArrayList<QuestionAnswer> questionAnswers, ArrayList<QuestionAnswer> nQuestionAnswers) {
        this.id = id;
        this.surveyId = surveyId;
        this.orderId = orderId;
        this.name = name;
        this.nodeList = nodeList;
        this.questionLayoutId = questionLayoutId;
        this.questionLayout = questionLayout;
        this.maxNodes = maxNodes;
        this.sortNodes = sortNodes;
        this.addNodes = addNodes;
        this.hideAddedNodes = hideAddedNodes;
        this.allowSelectAllNodes = allowSelectAllNodes;
        this.canSkip = canSkip;
        this.useDDSearch = useDDSearch;
        this.superUserViewNodes = superUserViewNodes;
        this.nodeSelectionId = nodeSelectionId;
        this.nodeSelection = nodeSelection;
        this.generatorName = generatorName;
        this.companies = companies;
        this.questions = questions;
        this.nodeQuestions = nodeQuestions;
        this.questionAnswers = questionAnswers;
        this.nQuestionAnswers = nQuestionAnswers;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public long getSurveyId() {
        return surveyId;
    }

    public void setSurveyId(long surveyId) {
        this.surveyId = surveyId;
    }

    public long getOrderId() {
        return orderId;
    }

    public void setOrderId(long orderId) {
        this.orderId = orderId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getNodeList() {
        return nodeList;
    }

    public void setNodeList(String nodeList) {
        this.nodeList = nodeList;
    }

    public long getQuestionLayoutId() {
        return questionLayoutId;
    }

    public void setQuestionLayoutId(long questionLayoutId) {
        this.questionLayoutId = questionLayoutId;
    }

    public QuestionFormat getQuestionLayout() {
        return questionLayout;
    }

    public void setQuestionLayout(QuestionFormat questionLayout) {
        this.questionLayout = questionLayout;
    }

    public int getMaxNodes() {
        return maxNodes;
    }

    public void setMaxNodes(int maxNodes) {
        this.maxNodes = maxNodes;
    }

    public boolean isSortNodes() {
        return sortNodes;
    }

    public void setSortNodes(boolean sortNodes) {
        this.sortNodes = sortNodes;
    }

    public boolean isAddNodes() {
        return addNodes;
    }

    public void setAddNodes(boolean addNodes) {
        this.addNodes = addNodes;
    }

    public boolean isHideAddedNodes() {
        return hideAddedNodes;
    }

    public void setHideAddedNodes(boolean hideAddedNodes) {
        this.hideAddedNodes = hideAddedNodes;
    }

    public boolean isAllowSelectAllNodes() {
        return allowSelectAllNodes;
    }

    public void setAllowSelectAllNodes(boolean allowSelectAllNodes) {
        this.allowSelectAllNodes = allowSelectAllNodes;
    }

    public boolean isCanSkip() {
        return canSkip;
    }

    public void setCanSkip(boolean canSkip) {
        this.canSkip = canSkip;
    }

    public boolean isUseDDSearch() {
        return useDDSearch;
    }

    public void setUseDDSearch(boolean useDDSearch) {
        this.useDDSearch = useDDSearch;
    }

    public boolean isSuperUserViewNodes() {
        return superUserViewNodes;
    }

    public void setSuperUserViewNodes(boolean superUserViewNodes) {
        this.superUserViewNodes = superUserViewNodes;
    }

    public int getNodeSelectionId() {
        return nodeSelectionId;
    }

    public void setNodeSelectionId(int nodeSelectionId) {
        this.nodeSelectionId = nodeSelectionId;
    }

    public QuestionFormat getNodeSelection() {
        return nodeSelection;
    }

    public void setNodeSelection(QuestionFormat nodeSelection) {
        this.nodeSelection = nodeSelection;
    }

    public String getGeneratorName() {
        return generatorName;
    }

    public void setGeneratorName(String generatorName) {
        this.generatorName = generatorName;
    }

    public Companies getCompanies() {
        return companies;
    }

    public void setCompanies(Companies companies) {
        this.companies = companies;
    }

    public ArrayList<Question> getQuestions() {
        return questions;
    }

    public void setQuestions(ArrayList<Question> questions) {
        this.questions = questions;
    }

    public ArrayList<Question> getNodeQuestions() {
        return nodeQuestions;
    }

    public void setNodeQuestions(ArrayList<Question> nodeQuestions) {
        this.nodeQuestions = nodeQuestions;
    }

    public ArrayList<QuestionAnswer> getQuestionAnswers() {
        return questionAnswers;
    }

    public void setQuestionAnswers(ArrayList<QuestionAnswer> questionAnswers) {
        this.questionAnswers = questionAnswers;
    }

    public ArrayList<QuestionAnswer> getnQuestionAnswers() {
        return nQuestionAnswers;
    }

    public void setnQuestionAnswers(ArrayList<QuestionAnswer> nQuestionAnswers) {
        this.nQuestionAnswers = nQuestionAnswers;
    }

    @Override
    public String toString() {
        return "Relationship{" +
                "id=" + id +
                ", surveyId=" + surveyId +
                ", orderId=" + orderId +
                ", name='" + name + '\'' +
                ", nodeList='" + nodeList + '\'' +
                ", questionLayoutId=" + questionLayoutId +
                ", questionLayout=" + questionLayout +
                ", maxNodes=" + maxNodes +
                ", sortNodes=" + sortNodes +
                ", addNodes=" + addNodes +
                ", hideAddedNodes=" + hideAddedNodes +
                ", allowSelectAllNodes=" + allowSelectAllNodes +
                ", canSkip=" + canSkip +
                ", useDDSearch=" + useDDSearch +
                ", superUserViewNodes=" + superUserViewNodes +
                ", nodeSelectionId=" + nodeSelectionId +
                ", nodeSelection=" + nodeSelection +
                ", generatorName='" + generatorName + '\'' +
                ", companies=" + companies +
                ", questions=" + questions +
                ", nodeQuestions=" + nodeQuestions +
                ", questionAnswers=" + questionAnswers +
                ", nQuestionAnswers=" + nQuestionAnswers +
                '}';
    }
}
