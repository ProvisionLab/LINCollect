//
//  SurveysCell.h
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "SurveyObject.h"
@interface SurveysCell : UITableViewCell

@property (strong, nonatomic) SurveyObject * curObject;

-(void) setCells;

@end
