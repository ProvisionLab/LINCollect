//
//  JobsDeteilView.m
//  Bounty
//
//  Created by Sergey on 4/1/16.
//  Copyright © 2016 user. All rights reserved.
//

#import "SurveyDeteilView.h"

@interface SurveyDeteilView ()
{
    UIViewController * parentViewController;
    UIView           * dialogView;
}
@end

@implementation SurveyDeteilView
@synthesize delegate;

+ (SurveyDeteilView *) initWithParrentView:(UIViewController *)parrentViewController
{
    SurveyDeteilView* view = [[SurveyDeteilView alloc] initWithParentViewController:parrentViewController];
    return view;
}
- (id) initWithParentViewController:(UIViewController *)parentController
{
    self = [super init];
    if (self) {
        parentViewController = parentController;
        self.frame = parentViewController.view.bounds;
        [parentViewController.view addSubview:self];
    }
    return self;
}

-(void) show
{
    NSArray * array = [[NSBundle mainBundle] loadNibNamed:@"SurveyDeteilView" owner:parentViewController options:nil];
    dialogView =  [array objectAtIndex:0];
    [dialogView setFrame:CGRectMake(8, 8, SCREEN_WIDTH - 16, 130)];
    UILabel * numberOfQuestions = (UILabel *)[dialogView viewWithTag:1];
    UILabel * titleOfQuestions = (UILabel *)[dialogView viewWithTag:2];
    
    numberOfQuestions.text = [NSString stringWithFormat:@"%i", self.numberOfQuestion];
    titleOfQuestions.text = self.titleOfQuestion;
    
    [delegate returnView:dialogView];
    
    for (UIView *v in [self subviews]) {
        [v removeFromSuperview];
    }
    [self removeFromSuperview];

}

@end
