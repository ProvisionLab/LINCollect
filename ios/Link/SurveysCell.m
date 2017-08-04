//
//  SurveysCell.m
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import "SurveysCell.h"
@interface SurveysCell()
{
    __weak IBOutlet UILabel *name;
    __weak IBOutlet UILabel *question;
    __weak IBOutlet UIView *mainView;
    
}
@end
@implementation SurveysCell

-(void) setCells {
    name.text = self.curObject.name;
    question.text = self.curObject.question;
    [self performSelector:@selector(setShadow) withObject:nil afterDelay:0.1];
    
}
-(void) setShadow {
    [mainView addShadow];
}


@end
