//
//  JobsDeteilView.h
//  Bounty
//
//  Created by Sergey on 4/1/16.
//  Copyright © 2016 user. All rights reserved.
//

#import <UIKit/UIKit.h>

@protocol SurveyDeteilViewDelegate
@optional
-(void) returnSimpleText:(NSString *)text forIndex:(int)index;


-(void) returnView:(UIView *)view;
@end

@interface SurveyDeteilView : UIView <SurveyDeteilViewDelegate>

+ (SurveyDeteilView *) initWithParrentView:(UIViewController *)parrentViewController;
-(void) show;
@property (assign, nonatomic) int numberOfQuestion;
@property (strong, nonatomic) NSString * titleOfQuestion;
@property (assign, nonatomic) int index;
@property (nonatomic, assign) id<SurveyDeteilViewDelegate> delegate;

@end
