//
//  UIView+CornerRadius.m
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import "UIView+CornerRadius.h"

@implementation UIView (CornerRadius)
-(void) cornerRadius:(int)radius {
    [self.layer setCornerRadius:radius];
}
@end
