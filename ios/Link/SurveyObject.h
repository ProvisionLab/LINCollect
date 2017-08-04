//
//  SurveysObject.h
//  Link
//
//  Created by Sergey on 4/21/16.
//  Copyright © 2016 Sergey. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface SurveyObject : NSObject

@property (strong, nonatomic) NSString * name;
@property (strong, nonatomic) NSString * question;
@property (assign, nonatomic) int questionCount;
@end
