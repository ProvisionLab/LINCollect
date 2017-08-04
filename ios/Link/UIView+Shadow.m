//
//  UIView+Shadow.m
//  ShadowDemo
//
//  Created by navy on 12-12-24.
//  Copyright (c) 2012年 navy. All rights reserved.
//

#import "UIView+Shadow.h"

@implementation UIView (Shadow)

- (void)addShadow
{
    self.layer.shadowColor = [UIColor blackColor].CGColor;
    self.layer.shadowOffset = CGSizeMake(1, 1);
    self.layer.shadowOpacity = 0.3;
    self.layer.shadowRadius = 2;
    self.layer.shadowPath = [[UIBezierPath bezierPathWithRect:self.layer.bounds] CGPath];
}

- (void)addRightShadow
{
    self.layer.shadowColor = [UIColor blackColor].CGColor;
    self.layer.shadowOffset = CGSizeMake(2, 5);
    self.layer.shadowOpacity = 0.5;
    self.layer.shadowRadius = 3;
    self.layer.shadowPath = [[UIBezierPath bezierPathWithRect:self.layer.bounds] CGPath];
}

-(void)addBottomShadow
{
    [self.layer setShadowOffset:CGSizeMake(0, 0)];
    [self.layer setShadowColor:[UIColor blackColor].CGColor];
    [self.layer setShadowRadius:1];
    [self.layer setShadowOpacity:0.3];
    [self.layer setMasksToBounds:NO];
    self.layer.shadowPath = [[UIBezierPath bezierPathWithRect:self.layer.bounds] CGPath];
}
@end
