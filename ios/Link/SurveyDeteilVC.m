//
//  JobsDeteilVC.m
//  Bounty
//
//  Created by Sergey on 4/1/16.
//  Copyright © 2016 user. All rights reserved.
//

#import "SurveyDeteilVC.h"
#import "SurveyDeteilView.h"
#import "UIView+Shadow.h"
@interface SurveyDeteilVC () <SurveyDeteilViewDelegate>
{
    int y;
    __weak IBOutlet UIScrollView *scrollView;
}

- (IBAction)submitButton:(UIButton *)sender;

@end

@implementation SurveyDeteilVC

- (void)viewDidLoad {
    [super viewDidLoad];
    [self setView];
    [self setData];
}

-(void) setView {
    [self.view setBackgroundColor:[UIColor whiteColor]];
}



-(void) setData {
    y = 0;
    [self performSelector:@selector(setBlocks) withObject:nil afterDelay:0.1];
}

-(void) setBlocks
{
    for (int i = 0; i < self.questionCount; i++) {
        [self setTextView:@"Some text question" andNumber:i];
    }
}


-(void) setTextView:(NSString *)title andNumber:(int)number
{
    SurveyDeteilView * jd = [SurveyDeteilView initWithParrentView:self];
    [jd setDelegate:self];
    [jd setTitleOfQuestion:title];
    [jd setNumberOfQuestion:number + 1];
    [jd show];
}

-(void) returnView:(UIView *)view
{
    CGRect viewRect = view.frame;
    viewRect.origin.y = y + 8;
    view.frame = viewRect;
    
    y += viewRect.size.height + 8;
    [scrollView setContentSize:CGSizeMake(SCREEN_WIDTH, y + 5)];
    [view addShadow];
    [scrollView addSubview:view];
}


- (IBAction)submitButton:(UIButton *)sender {
    
    UIView * view = [[UIView alloc] initWithFrame:CGRectMake(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT)];
    [view setBackgroundColor:[UIColor blackColor]];
    [view setAlpha:0.5];
    [self.view addSubview:view];
    UIActivityIndicatorView * indicator = [[UIActivityIndicatorView alloc] initWithActivityIndicatorStyle:UIActivityIndicatorViewStyleWhite];
    indicator.center = view.center;
    [view addSubview:indicator];
    [indicator startAnimating];
    
    [self performSelector:@selector(backVC) withObject:nil afterDelay:2];
    
    NSLog(@"CLICK");
}

-(void) backVC
{
    [self.navigationController popViewControllerAnimated:YES];
}

@end
