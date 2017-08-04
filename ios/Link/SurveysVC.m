//
//  SurveysVC.m
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import "SurveysVC.h"
#import "SurveyObject.h"
#import "SurveysCell.h"
#import "SurveyDeteilVC.h"
@interface SurveysVC () <UITableViewDelegate, UITableViewDataSource>
{
    BOOL isPush;
    NSMutableArray * surveysData;
}
@property (weak, nonatomic) IBOutlet UITableView *tableView;
@end

@implementation SurveysVC

#pragma mark LifeCicle
- (void)viewDidLoad {
    [super viewDidLoad];
    [self setView];
    [self setNavigation];
    [self setData];
}

-(void) viewDidAppear:(BOOL)animated {
    isPush = NO;
}

-(void) viewWillDisappear:(BOOL)animated {
    if (!isPush) {
        [self.navigationController setNavigationBarHidden:YES animated:YES];
    }
}

#pragma mark Methods
-(void) setData {
    surveysData = [NSMutableArray new];
    for (int i =  0; i < 3; i++) {
        SurveyObject * surveys = [[SurveyObject alloc] init];
        surveys.name = [NSString stringWithFormat:@"Survey %i", i];
        int random = arc4random_uniform(7);
        surveys.question = [NSString stringWithFormat:@"%i questions", random == 0?1:random];
        surveys.questionCount = random == 0?1:random;
        [surveysData addObject:surveys];
    }
}

-(void) setView {
    
}

-(void) setNavigation {
    self.navigationItem.title = @"Surveys";
    [self.navigationController setNavigationBarHidden:NO];
}

#pragma mark UITableViewDataSource
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return [surveysData count];
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    static  NSString * identifier = @"c";
    
    SurveysCell *cell = [tableView dequeueReusableCellWithIdentifier:identifier];
    
    if (cell == nil) {
        cell = [[SurveysCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:identifier];
    }
    
    cell.curObject = [surveysData objectAtIndex:indexPath.row];
    [cell setCells];
    
    return cell;
}

#pragma mark UITableViewDelegate
-(void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath
{
    isPush = YES;
    SurveysCell * selectedCell = [tableView cellForRowAtIndexPath:indexPath];
    [tableView deselectRowAtIndexPath:indexPath animated:NO];
    SurveyDeteilVC *vc = [self.storyboard instantiateViewControllerWithIdentifier:@"SurveyDeteilVC"];
    vc.questionCount = selectedCell.curObject.questionCount;
    [self.navigationController pushViewController:vc animated:YES];
}


@end
