//
//  UITextField+PlaceholderColor.m
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import "UITextField+PlaceholderColor.h"

@implementation UITextField (PlaceholderColor)

-(void) setPlaceholderColor {
    self.attributedPlaceholder = [[NSAttributedString alloc] initWithString:self.placeholder attributes:@{NSForegroundColorAttributeName: [UIColor grayColor]}];
    [self setLeftView:[[UIView alloc] initWithFrame:CGRectMake(0, 0, 7, self.frame.size.height)]];
    [self setLeftViewMode:UITextFieldViewModeAlways];
}

@end
